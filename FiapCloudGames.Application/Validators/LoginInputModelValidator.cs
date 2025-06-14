﻿using FiapCloudGames.Application.InputModels;
using FluentValidation;

namespace FiapCloudGames.Application.Validators;

public class LoginInputModelValidator : AbstractValidator<LoginInputModel> {
    public LoginInputModelValidator() {
        this.RuleLevelCascadeMode = CascadeMode.Stop;

        this.RuleFor(user => user.Email)
            .NotEmpty().WithMessage("O e-mail precisa ser informado!")
            .MaximumLength(60).WithMessage("O e-mail não pode exceder 60 caracteres!")
            .EmailAddress().WithMessage("Informe um e-mail válido!");

        this.RuleFor(user => user.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .Matches(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[^\w\s]).{8,}$")
            .WithMessage("A senha deve ter no mínimo 8 caracteres e conter letras, números e caracteres especiais.");
    }
}
