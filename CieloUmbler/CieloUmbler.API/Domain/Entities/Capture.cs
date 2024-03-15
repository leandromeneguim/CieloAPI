using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Entities
{
    public class Capture : BaseEntity
    {
        public string MerchantOrderId { get; private set; }
        public Customer Customer { get; private set; }
        public Payment Payment { get; private set; }

        public Capture(string merchantOrderId, Payment payment, Customer customer = null)
        {
            MerchantOrderId = merchantOrderId;
            Payment = payment;
            Customer = customer;
        }


        public override bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}
