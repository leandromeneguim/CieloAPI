using CieloUmbler.API.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Validators
{
    public class CardValidator : AbstractValidator<Card>
    {
        public CardValidator()
        {
            RuleFor(x => x)
                .NotEmpty()
                .WithMessage("A entidade não pode ser vazia");

            RuleFor(x => x.CardNumber)
                .NotNull()
                .WithMessage("O numero do cartão não pode ser nulo")
                .NotEmpty()
                .WithMessage("O numero do cartão deve ser preenchido")
                .CreditCard()
                .WithMessage("Número de cartão inválido")
                .MinimumLength(16)
                .WithMessage("O numero do cartão deve possuir 16 numeros")
                .When(x => x.CardNumber.StartsWith("4") || x.CardNumber.StartsWith("5"))
                .WithMessage("Somente aceitamos MasterCard ou Visa. Demais bandeiras não são aceitas");

            RuleFor(x => x.ExpirationDate)
                .NotNull()
                .WithMessage("A validade do cartão não pode ser nulo")
                .NotEmpty()
                .WithMessage("A validade do cartão deve ser preenchido")
                .Matches("^(0[1-9]|1[0-2])/?([0-9]{2})$")
                .WithMessage("Formato inválido");

            RuleFor(x => x.Brand)
                .NotNull()
                .WithMessage("A bandeira do cartão não pode ser nulo")
                .NotEmpty()
                .WithMessage("A bandeira do cartão deve ser preenchido");

            RuleFor(x => x.SecurityCode)
                .NotNull()
                .WithMessage("O código de segurança não pode ser nulo")
                .NotEmpty()
                .WithMessage("O código de segurança deve ser preenchido")
                .Length(3)
                .WithMessage("O código de segurança deve ter exatamente 03 digitos");

        }
    }
}
