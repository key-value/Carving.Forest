using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF.Data;
using Carving.Infrastructrue.Aop;

namespace Carving.Application.Services.Qrcode
{
    [Injection(typeof(IAddQrCodeServices))]
    public class AddQrCodeServices : ApplicationServices, IAddQrCodeServices
    {
        public IQrCodeRepository QrCodeRepository { private get; set; }


        public int Create(string code, Guid tableID)
        {
            var qrCode = new QrCode();
            qrCode.ID = Guid.NewGuid();
            qrCode.Code = code;
            qrCode.TableID = tableID;
            QrCodeRepository.Add(qrCode);
            RepositoryContext.Commit();
            return 1;
        }
    }
}
