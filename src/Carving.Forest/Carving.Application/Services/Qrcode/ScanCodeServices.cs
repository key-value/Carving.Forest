
using System;
using Carving.Domain.Core.Specifications;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF;
using Carving.Domain.Repository.EF.Data;
using Carving.Infrastructrue.Aop;
using Carving.Infrastructrue.Autofac;

namespace Carving.Application
{
    [Injection(typeof(IScanCodeServices))]
    public class ScanCodeServices : IScanCodeServices
    {
        public ITableRepository TableRepository { private get; set; }


        public int ScanCode()
        {
            if (TableRepository == null)
            {
                //TableRepository = ServiceLocator.Instance.GetService<ITableRepository>();
                if (TableRepository == null)
                {

                }
                return 0;
            }
            TableRepository.Find(Specification<Table>.Eval(x => x.Name == "xxx"));
            return 1;
        }
    }
}
