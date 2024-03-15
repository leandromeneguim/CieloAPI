using CieloUmbler.API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public string Type { get; private set; }
        public decimal Amount { get; private set; }
        public int Installments { get; private set; }
        public Guid PaymentId { get; private set; }
        public Card CreditCard { get; private set; }


        public Payment()
        {
            Installments = 0;
        }

        public Payment(string type, decimal amount, int installments, Card card)
        {
            Type = type;
            Amount = amount;
            Installments = installments;
            CreditCard = card;
        }


        public void ReceivePaymentId(Guid paymentId)
        {
            if (PaymentId == Guid.Empty)
                throw new Exception("Valor do PaymentID não pode ser nulo");

            PaymentId = paymentId;
        }



        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
