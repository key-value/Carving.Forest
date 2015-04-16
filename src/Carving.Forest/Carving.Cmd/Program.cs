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

            var builder = new ContainerBuilder();
            builder.RegisterType<EntityFrameworkRepositoryContext>().As<IRepositoryContext>().PropertiesAutowired();
            builder.RegisterType<TableRepository>().As<ITableRepository>().PropertiesAutowired();
            builder.RegisterType<ScanCodeServices>().As<IScanCodeServices>().PropertiesAutowired();

            IContainer container = builder.Build();

            var scanCodeServices = ServiceLocator.Instance.GetService<IScanCodeServices>();
            scanCodeServices.ScanCode();
        }
    }
}
