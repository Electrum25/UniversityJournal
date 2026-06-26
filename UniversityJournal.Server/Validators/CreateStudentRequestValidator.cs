using FluentValidation;
using UniversityJournal.Core.UseCases;
using static UniversityJournal.Core.UseCases.CreateStudentUseCase;

namespace UniversityJournal.Server.Validators
{
    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Имя обязательно")
                .Length(2, 50).WithMessage("Имя должно быть от 2 до 50 символов");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Фамилия обязательна");
            RuleFor(x => x.GroupId)
                .NotEqual(Guid.Empty).WithMessage("Группа обязательна");
        }
    }
}