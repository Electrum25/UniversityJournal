using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using OpenIddict.Validation.AspNetCore;
using System.Security.Claims;
using UniversityJournal.Core.Entities;
using UniversityJournal.EfCore;
using UniversityJournal.Identity;
using static OpenIddict.Abstractions.OpenIddictConstants;
using UniversityJournal.Core.Identity;

namespace UniversityJournal.Server.Controllers
{
    [Route("connect")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly SignInManager<UniversityJournalIdentityUser> _signInManager;
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly UniversityJournalDbContext _businessContext;

        public AuthorizationController(
            SignInManager<UniversityJournalIdentityUser> signInManager,
            UserManager<UniversityJournalIdentityUser> userManager,
            ILogger<AuthorizationController> logger,
            UniversityJournalDbContext businessContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _businessContext = businessContext;
        }

        [HttpGet("authorize")]
        [HttpPost("authorize")]
        [Authorize(AuthenticationSchemes = "Identity.Application")]
        public async Task<IActionResult> Authorize()
        {
            var request = HttpContext.GetOpenIddictServerRequest();
            if (request == null)
                return BadRequest("Invalid OpenIddict request");

            if (!User.Identity.IsAuthenticated)
            {
                var properties = new AuthenticationProperties
                {
                    RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
                        Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
                };
                return Challenge(properties, IdentityConstants.ApplicationScheme);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Forbid();

            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            var identity = principal.Identity as ClaimsIdentity;

            var claimsToRemove = identity.FindAll(c =>
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == OpenIddictConstants.Claims.Subject).ToList();
            foreach (var claim in claimsToRemove)
                identity.RemoveClaim(claim);

            identity.AddClaim(new Claim(OpenIddictConstants.Claims.Subject, user.Id.ToString()));

            principal.SetScopes(request.GetScopes());

            _logger.LogInformation("Principal claims after adding sub: {Claims}",
                principal.Claims.Select(c => $"{c.Type}: {c.Value}"));

            return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
        }

        [HttpPost("token"), AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                {
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                var principal = await _signInManager.CreateUserPrincipalAsync(user);
                principal.SetClaim(OpenIddictConstants.Claims.Subject, user.Id.ToString());

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    principal.AddClaim(OpenIddictConstants.Claims.Role, role);
                }
                principal.SetScopes(request.GetScopes());

                foreach (var claim in principal.Claims)
                {
                    claim.SetDestinations(GetDestinations(claim, principal));
                }

                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }
            
            if (request.IsAuthorizationCodeGrantType() || request.IsRefreshTokenGrantType())
            {
                var result = await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var userId = result.Principal.GetClaim(OpenIddictConstants.Claims.Subject);
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return Forbid(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
                }

                var principal = await _signInManager.CreateUserPrincipalAsync(user);

                principal.SetClaim(OpenIddictConstants.Claims.Subject, await _userManager.GetUserIdAsync(user));

                var roles = await _userManager.GetRolesAsync(user);
                foreach (var role in roles)
                {
                    principal.AddClaim(OpenIddictConstants.Claims.Role, role);
                }
                principal.SetScopes(request.GetScopes());

                foreach (var claim in principal.Claims)
                {
                    claim.SetDestinations(GetDestinations(claim, principal));
                }

                return SignIn(principal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            throw new InvalidOperationException("The specified grant type is not supported.");
        }

        private static IEnumerable<string> GetDestinations(Claim claim, ClaimsPrincipal principal)
        {
            switch (claim.Type)
            {
                case OpenIddictConstants.Claims.Name:
                case OpenIddictConstants.Claims.Email:
                case OpenIddictConstants.Claims.Role:
                    yield return OpenIddictConstants.Destinations.AccessToken;
                    yield return OpenIddictConstants.Destinations.IdentityToken;
                    yield break;

                default:
                    yield return OpenIddictConstants.Destinations.AccessToken;
                    yield break;
            }
        }

        [HttpGet("~/connect/logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            var request = HttpContext.GetOpenIddictServerRequest();

            if (request == null)
            {
                return Redirect("/connect/login");
            }

            return SignOut(
                authenticationSchemes: OpenIddictServerAspNetCoreDefaults.AuthenticationScheme,
                properties: new AuthenticationProperties
                {
                    RedirectUri = "/connect/login"
                });
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            var encodedReturnUrl = returnUrl != null
                ? System.Web.HttpUtility.UrlEncode(returnUrl)
                : "/";

            var html = $@"
    <html>
    <body>
        <h2>Login</h2>
        <form method='post' action='/connect/login'>
            <input type='hidden' name='ReturnUrl' value='{encodedReturnUrl}' />
            <div>
                <label>Username:</label>
                <input type='text' name='username' value='admin' />
            </div>
            <div>
                <label>Password:</label>
                <input type='password' name='password' value='Admin123!' />
            </div>
            <button type='submit'>Login</button>
        </form>
    </body>
    </html>";

            return Content(html, "text/html");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> LoginPost(
            [FromForm] string username,
            [FromForm] string password,
            [FromForm] string ReturnUrl)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return BadRequest("Username and password are required");

            var user = await _userManager.FindByNameAsync(username);
            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                _logger.LogInformation($"User {username} signed in. IsAuthenticated: {User.Identity?.IsAuthenticated}");

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    var decodedUrl = System.Web.HttpUtility.UrlDecode(ReturnUrl);
                    return Redirect(decodedUrl);
                }
                return Redirect("/");
            }

            return Content("Invalid login attempt", "text/html");
        }

        [HttpGet("token-test")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenDirect(string username, string password)
        {
            if (username != "admin" || password != "Admin123!")
                return Unauthorized("Invalid credentials");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return Unauthorized("User not found");

            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            var ticket = new AuthenticationTicket(principal,
                new AuthenticationProperties(),
                OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

            return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
        }

        [HttpGet("~/connect/userinfo")]
        [Authorize(AuthenticationSchemes = OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Userinfo()
        {
            var userId = User.GetClaim(OpenIddictConstants.Claims.Subject);
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return Challenge(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            var claims = new Dictionary<string, object>(StringComparer.Ordinal)
            {
                [OpenIddictConstants.Claims.Subject] = await _userManager.GetUserIdAsync(user),
                [OpenIddictConstants.Claims.Name] = user.UserName,
                [OpenIddictConstants.Claims.Email] = user.Email
            };

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Any())
            {
                claims[OpenIddictConstants.Claims.Role] = roles;
            }

            return Ok(claims);
        }

        [HttpGet("/callback")]
        [AllowAnonymous]
        public IActionResult Callback(string code)
        {
            return Content($"Код получен: {code}. Скопируйте его в приложение, если оно не перехватило его автоматически.");
        }
    }
}