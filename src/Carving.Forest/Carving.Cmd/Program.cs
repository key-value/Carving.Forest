using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Carving.Application;
using Carving.Infrastructrue.Autofac;

namespace Carving.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var scanCodeServices = ServiceLocator.Instance.GetService<IScanCodeServices>();
            scanCodeServices.ScanCode();
        }
    }
}
