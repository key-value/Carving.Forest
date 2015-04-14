using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carving.Domain.Core;

namespace Carving.Domain.Model
{
    public class Table : IAggregateRoot
    {
        public Guid ID { get; set; }

        public string Name { get; set; }
    }
}
