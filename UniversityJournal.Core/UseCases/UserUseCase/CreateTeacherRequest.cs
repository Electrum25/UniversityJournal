using System.ComponentModel.DataAnnotations;

namespace UniversityJournal.Core.UseCases
{
    public class CreateTeacherRequest
    {
        [Required(ErrorMessage = "Логин обязателен")]
        [StringLength(50)]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(6, ErrorMessage = "Пароль должен быть не менее 6 символов")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя обязательно")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Фамилия обязательна")]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        public string? Patronymic { get; set; }
    }
}