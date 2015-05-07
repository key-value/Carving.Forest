using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carving.Domain.Core.Event;
using Carving.Domain.Events.Handlers;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF.Data;
using Carving.Infrastructrue.Aop;

namespace Carving.Domain.Events.Handles
{
    [HandlesAsynchronously]
    [Injection(typeof(GetQrCodeTableEvent))]
    public class GetQrCodeTableEventHandle : IDomainEventHandler<GetQrCodeTableEvent>
    {
        public ITableRepository TableRepository { get; set; }

        #region Implementation of IEventHandler<in GetQrCodeTableEvent>

        /// <summary>
        /// 处理给定的事件。
        /// </summary>
        /// <param name="evnt">需要处理的事件。</param>
        public void Handle(GetQrCodeTableEvent evnt)
        {
            var qrCode = evnt.Source as QrCode;
            if (qrCode == null)
            {
                return;
            }
            evnt.Table = TableRepository.GetByKey(qrCode.TableID);
        }

        #endregion
    }
}
