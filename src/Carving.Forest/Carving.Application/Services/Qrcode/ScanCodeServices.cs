
using System;
using Carving.Domain.Core.Specifications;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF;
using Carving.Domain.Repository.EF.Data;
using Carving.Infrastructrue.Aop;

namespace Carving.Application
{
    [Injection(typeof(IScanCodeServices))]
    public class ScanCodeServices : IScanCodeServices
    {
        public ITableRepository TableRepository { get; set; }

        public IQrCodeRepository QrCodeRepository { get; set; }

        public QrCode ScanCode()
        {
            return QrCodeRepository.GetByKey(new Guid("5C188B3F-E0F9-4F6D-9ACA-F05B29ABC960"));
        }
    }
}
