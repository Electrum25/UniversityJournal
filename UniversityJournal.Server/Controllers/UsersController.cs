using Microsoft.AspNetCore.Mvc;
using UniversityJournal.Core.DTOs;
using UniversityJournal.Core.Entities;
using UniversityJournal.Core.UseCases;
using UniversityJournal.Core.Repositories;
using Microsoft.AspNetCore.Authorization; // Обязательно

namespace UniversityJournal.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITeacherRepository _teacherRepository;

        public UsersController(IUserRepository userRepository,
            IStudentRepository studentRepository,
            ITeacherRepository teacherRepository)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// Вход в систему. Доступ открыт для всех [AllowAnonymous].
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(
    [FromBody] AuthenticateUserUseCase.AuthenticateUserRequest request,
    [FromServices] AuthenticateUserUseCase authenticateUseCase)
        {
            try
            {
                var authData = await authenticateUseCase.Handle(request);
                if (authData == null) return Unauthorized("Неверный логин или пароль");

                Guid? profileId = null;

                if (authData.User.Role == UserRole.Teacher)
                {
                    var teachers = await _teacherRepository.GetAll();
                    // Обращаемся к TeacherId вместо Id
                    profileId = teachers?.FirstOrDefault(t => t.UserId == authData.User.UserId)?.TeacherId;
                }
                else if (authData.User.Role == UserRole.Student)
                {
                    var students = await _studentRepository.GetAll();
                    // Обращаемся к StudentId вместо Id
                    profileId = students?.FirstOrDefault(s => s.UserId == authData.User.UserId)?.StudentId;
                }

                return Ok(new
                {
                    UserId = authData.User.UserId,
                    Login = authData.User.Login,
                    Role = authData.User.Role.ToString(),
                    ProfileId = profileId // Это значение уйдет во Flutter
                });
            }
            catch (ArgumentException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// Регистрация. Обычно выполняется администратором.
        /// </summary>
        [HttpPost("register-student")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateStudent(
            [FromBody] CreateStudentUseCase.CreateStudentRequest request,
            [FromServices] CreateStudentUseCase createStudentUseCase)
        {
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
            try
            {
                var teacherId = await createTeacherUseCase.Handle(request);
                return Ok(new { TeacherId = teacherId });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Список пользователей. Только для администрации.
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAll();
            if (users == null) return Ok(new List<UserDTO>());
            return Ok(users.Select(u => new UserDTO(u)));
        }

        /// <summary>
        /// Профили. Доступны владельцам или админу.
        /// </summary>
        [HttpGet("student-profile/{userId}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentProfile(Guid userId)
        {
            var students = await _studentRepository.GetAll();
            var student = students?.FirstOrDefault(s => s.UserId == userId);
            if (student == null) return NotFound("Профиль студента не найден.");
            return Ok(student);
        }

        [HttpGet("teacher-profile/{userId}")]
        [Authorize(Roles = "Admin,Teacher")]
        public async Task<IActionResult> GetTeacherProfile(Guid userId)
        {
            var teachers = await _teacherRepository.GetAll();
            var teacher = teachers?.FirstOrDefault(t => t.UserId == userId);
            if (teacher == null) return NotFound("Профиль преподавателя не найден.");
            return Ok(teacher);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.Get(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Полное удаление. Только Админ.
        /// </summary>
        [HttpDelete("{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(
            Guid userId,
            [FromServices] DeleteUserUseCase deleteUserUseCase)
        {
            try
            {
                await deleteUserUseCase.Handle(userId);
                return Ok("Пользователь успешно удален");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление. Доступно Админу или соответствующей роли.
        /// </summary>
        [HttpPut("student")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> UpdateStudent(
            [FromBody] UpdateStudentUseCase.UpdateStudentRequest request,
            [FromServices] UpdateStudentUseCase updateStudentUseCase)
        {
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
    }
}