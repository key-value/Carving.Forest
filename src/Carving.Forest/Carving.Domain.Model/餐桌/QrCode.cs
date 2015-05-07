using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Carving.Domain.Core;
using Carving.Domain.Core.Event;
using Carving.Domain.Events;
using Carving.Domain.Events.Handlers;

namespace Carving.Domain.Model
{
    public class QrCode : IAggregateRoot
    {
        public Guid ID { get; set; }

        public string Code { get; set; }

        public Guid TableID { get; set; }


        public Table GetTable
        {
            get
            {
                Table result = null;
                DomainEvent.Publish<GetQrCodeTableEvent>(new GetQrCodeTableEvent(this),
                    (e, ret, exc) =>
                    {
                        result = e.Table;
                    });
                return result;
            }
        }
    }
}
