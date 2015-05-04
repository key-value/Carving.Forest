// Carving.Infrastructrue InjectionAttribute.cs
// writer sundy
// Last Update Time 2015-04-16-16:27
// Create Time 2015-04-16-16:27

using System;

namespace Carving.Infrastructrue.Aop
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InjectionAttribute : System.Attribute
    {
        public InjectionAttribute(Type interfaceName)
        {
            this.InterfaceName = interfaceName;
        }

        public Type InterfaceName
        {
            get;
            set;
        }
    }
}