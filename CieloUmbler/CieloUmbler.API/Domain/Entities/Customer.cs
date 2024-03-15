using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Entities
{
    public class Customer
    {
        public string Name { get; private set; }
        public string Identity { get; private set; }

        public Customer(string name, string identity)
        {
            Name = name;
            Identity = identity;
        }

    }
}
