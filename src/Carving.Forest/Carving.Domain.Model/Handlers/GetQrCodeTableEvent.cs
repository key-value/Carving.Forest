using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Carving.Domain.Core;
using Carving.Domain.Core.Event;
using Carving.Domain.Model;

namespace Carving.Domain.Events.Handlers
{
    [HandlesAsynchronously]
    public class GetQrCodeTableEvent : DomainEvent
    {
        public GetQrCodeTableEvent(IEntity source) : base(source) { }

        public Table Table { get; set; }


    }
}
