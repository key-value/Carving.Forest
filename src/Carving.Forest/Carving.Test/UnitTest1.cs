using System;
using System.Collections.Generic;
using Carving.Application;
using Carving.Domain.Model;
using Carving.Domain.Repository.EF.Data;
using Carving.Infrastructrue.Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Xunit;

namespace Carving.Test
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            var tableRepository = new Mock<ITableRepository>();
            tableRepository.Setup(x => x.FindAll()).Returns(new List<Table>());
            var a = tableRepository.Object.FindAll();

            var scanCodeServices = ServiceLocator.Instance.GetService<IScanCodeServices>();
            scanCodeServices.ScanCode();
        }
    }
}
