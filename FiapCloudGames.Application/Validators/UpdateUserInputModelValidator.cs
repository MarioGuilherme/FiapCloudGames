using FiapCloudGames.Application.InputModels;
using FluentValidation;

namespace FiapCloudGames.Application.Validators;

public class UpdateUserInputModelValidator : AbstractValidator<UpdateUserInputModel> {
    public UpdateUserInputModelValidator() {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(user => user.Name)
            .NotEmpty().WithMessage("O nome precisa ser informado!")
            .MaximumLength(60).WithMessage("O nome não pode exceder 60 caracteres!");

        this.RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail precisa ser informado!")
            .MaximumLength(60).WithMessage("O e-mail não pode exceder 60 caracteres!")
            .EmailAddress().WithMessage("Informe um e-mail válido!");
    }
}
