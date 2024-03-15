using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; private set; }
        internal List<string> _errors { get; set; }

        public abstract bool Validate();
    }
}
