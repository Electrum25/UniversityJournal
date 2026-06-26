using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Validation.AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.Identity;
using UniversityJournal.Core.Repositories;
using UniversityJournal.Core.UseCases;
using static UniversityJournal.Core.UseCases.CreateStudentUseCase;

namespace UniversityJournal.Server.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly UserManager<UniversityJournalIdentityUser> _userManager;

        public UsersController(
            IUserRepository userRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository,
            UserManager<UniversityJournalIdentityUser> userManager)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _userManager = userManager;
        }

        [HttpPost("register-student")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(
    [FromBody] CreateStudentUseCase.CreateStudentRequest request,
    [FromServices] CreateStudentUseCase createStudentUseCase,
    [FromServices] IValidator<CreateStudentUseCase.CreateStudentRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            try
            {
                var studentId = await createStudentUseCase.Handle(request);
                return Ok(new { StudentId = studentId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-teacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTeacher(
    [FromBody] CreateTeacherUseCase.CreateTeacherRequest request,
    [FromServices] CreateTeacherUseCase createTeacherUseCase)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await createTeacherUseCase.Handle(request);
            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });
            return Ok(new { TeacherId = result.Value });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll([FromServices] GetAllUsersUseCase useCase)
        {
            var dtos = await useCase.Handle();
            return Ok(dtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id, [FromServices] GetUserByIdUseCase useCase)
        {
            var dto = await useCase.Handle(id);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        [HttpGet("student-profile/{userId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentProfile(Guid userId, [FromServices] GetStudentProfileUseCase useCase)
        {
            var dto = await useCase.Handle(userId);
            if (dto == null) return NotFound("Профиль студента не найден.");
            return Ok(dto);
        }

        [HttpGet("teacher-profile/{userId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetTeacherProfile(Guid userId, [FromServices] GetTeacherProfileUseCase useCase)
        {
            var dto = await useCase.Handle(userId);
            if (dto == null) return NotFound("Профиль преподавателя не найден.");
            return Ok(dto);
        }

        

        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
    Guid userId,
    [FromServices] DeleteUserUseCase deleteUserUseCase)
        {
            var result = await deleteUserUseCase.Handle(userId);
            if (!result.IsSuccess)
                return BadRequest(new { error = result.Error });
            return Ok("Пользователь успешно удалён");
        }

        [HttpPut("student")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudent(
    [FromBody] UpdateStudentUseCase.UpdateStudentRequest request,
    [FromServices] UpdateStudentUseCase updateStudentUseCase)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(request.FirstName))
                errors.Add("Имя не может быть пустым");
            if (string.IsNullOrWhiteSpace(request.LastName))
                errors.Add("Фамилия не может быть пустой");
            if (request.StudentId == Guid.Empty)
                errors.Add("Не указан ID студента");
            if (errors.Any())
                return BadRequest(new { errors });

            try
            {
                await updateStudentUseCase.Handle(request);
                return Ok("Данные студента обновлены");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("teacher")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> UpdateTeacher(
            [FromBody] UpdateTeacherUseCase.UpdateTeacherRequest request,
            [FromServices] UpdateTeacherUseCase updateTeacherUseCase)
        {
            try
            {
                await updateTeacherUseCase.Handle(request);
                return Ok("Данные преподавателя обновлены");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("by-identity/{identityId}")]
        public async Task<IActionResult> GetUserByIdentity(Guid identityId, [FromServices] GetUserByIdentityUseCase useCase)
        {
            var dto = await useCase.Handle(identityId);
            if (dto == null) return NotFound("Пользователь не найден.");
            return Ok(dto);
        }

        [HttpGet("student-profile/by-identity/{identityId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentProfileByIdentity(Guid identityId, [FromServices] GetStudentProfileUseCase useCase)
        {
            var dto = await useCase.HandleByIdentity(identityId);
            if (dto == null) return NotFound("Профиль студента не найден.");
            return Ok(dto);
        }

        [HttpGet("teacher-profile/by-identity/{identityId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetTeacherProfileByIdentity(Guid identityId, [FromServices] GetTeacherProfileUseCase useCase)
        {
            var dto = await useCase.HandleByIdentity(identityId);
            if (dto == null) return NotFound("Профиль преподавателя не найден.");

            return Ok(dto);
        }
    }
}