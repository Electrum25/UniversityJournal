using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.UseCases;

namespace UniversityJournal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthenticateUserUseCase _authUseCase;

        public AuthController(AuthenticateUserUseCase authUseCase)
        {
            _authUseCase = authUseCase;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Данные обязательны.");
            }

            // Создаем объект запроса, который ожидает твой UseCase
            var authRequest = new AuthenticateUserUseCase.AuthenticateUserRequest
            {
                Login = request.Email, // Используем Email как Login
                Password = request.Password
            };

            // Аутентификация через твой UseCase
            var authData = await _authUseCase.Handle(authRequest);

            if (authData == null)
            {
                return Unauthorized(new { message = "Неверный логин или пароль" });
            }

            // Создаем claims для пользователя (без Email, так как его нет в модели)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, authData.User.UserId.ToString()),
                new Claim(ClaimTypes.Name, authData.User.Login), // Используем Login как Name
                new Claim(ClaimTypes.Role, authData.User.Role.ToString()),
                new Claim("Login", authData.User.Login) // Добавляем Login отдельным claim
            };

            // Добавляем BusinessId если есть
            if (authData.BusinessId.HasValue)
            {
                claims.Add(new Claim("BusinessId", authData.BusinessId.Value.ToString()));
            }

            // Создаем identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Свойства аутентификации
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            // Выполняем вход (создаем cookie)
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            // Возвращаем информацию о пользователе
            return Ok(new AuthResponse
            {
                Success = true,
                Role = authData.User.Role.ToString(),
                UserId = authData.User.UserId,
                BusinessId = authData.BusinessId,
                Username = authData.User.Login
            });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Выход выполнен успешно" });
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            if (!User.Identity?.IsAuthenticated ?? false)
            {
                return Unauthorized();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            var login = User.FindFirst("Login")?.Value;
            var businessId = User.FindFirst("BusinessId")?.Value;

            return Ok(new
            {
                UserId = userId,
                Username = username,
                Login = login,
                Role = role,
                BusinessId = businessId
            });
        }

        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            return Ok(new { isAuthenticated = User.Identity?.IsAuthenticated ?? false });
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Guid? BusinessId { get; set; }
    }
}