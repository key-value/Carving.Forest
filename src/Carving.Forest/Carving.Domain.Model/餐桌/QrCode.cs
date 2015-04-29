using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Carving.Domain.Core;

namespace Carving.Domain.Model
{
    public class QrCode : IAggregateRoot
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid TableID { get; set; }
    }
}
