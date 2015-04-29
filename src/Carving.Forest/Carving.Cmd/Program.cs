using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Carving.Application;
using Carving.Domain.Core.Repositories;
using Carving.Domain.Repository.EF.Data;
using Carving.Domain.Repository.EF.Repository;
using Carving.Infrastructrue.Autofac;

namespace Carving.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var addQrCodeServices = ServiceLocator.Instance.GetService<IAddQrCodeServices>();
            addQrCodeServices.Create("xxxfddfd", Guid.NewGuid());
        }
    }
}
