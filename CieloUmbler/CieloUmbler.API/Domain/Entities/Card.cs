using CieloUmbler.API.Domain.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Entities
{
    public class Card : BaseEntity
    {
        [Display(Name = "Nome do cliente", AutoGenerateFilter = false)]
        public string CustomerName { get; private set; }

        [Display(Name = "Nome impresso no cartão", AutoGenerateFilter = false)]
        public string Holder { get; private set; }

        [Display(Name = "Número do Cartão", AutoGenerateFilter = false)]
        public string CardNumber { get; private set; }

        [Display(Name = "Validade do cartão", AutoGenerateFilter = false)]
        public string ExpirationDate { get; private set; }

        [Display(Name = "Código de Segurança", AutoGenerateFilter = false)]
        public string SecurityCode { get; private set; }

        [Display(Name = "Bandeira", AutoGenerateFilter = false)]
        public string Brand { get; private set; }


        public Card()
        {
            CustomerName = "";
            Holder = "";
            CardNumber = "";
            ExpirationDate = "";
            SecurityCode = "";
            Brand = "";
        }

        public Card(string customer, string holder, string cardNumber, string expirationDate, string securityCode, string brand)
        {
            CustomerName = customer;
            Holder = holder;
            CardNumber = cardNumber;
            ExpirationDate = expirationDate;
            SecurityCode = securityCode;
            Brand = brand;
        }

        public string GetCardBin()
        {
            return CardNumber.Substring(0, 6);
        }

        public override bool Validate()
        {
            var validator = new CardValidator();
            var validation = validator.Validate(this);

            if (!validation.IsValid)
            {
                foreach (var error in validation.Errors)
                {
                    _errors.Add(error.ErrorMessage);
                }
                throw new Exception("Alguns campos estão inválidos. Corrija-os: " + _errors);
            }

            return true;
        }
    }
}
