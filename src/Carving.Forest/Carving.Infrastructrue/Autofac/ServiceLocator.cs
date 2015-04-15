using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;
using Autofac;
using Autofac.Builder;

namespace Carving.Infrastructrue.Autofac
{
    public class ServiceLocator
    {
        private static readonly ServiceLocator instance = new ServiceLocator();

        private readonly IContainer _container;
        /// <summary>
        /// Initializes a new instance of <c>ServiceLocator</c> class.
        /// </summary>
        private ServiceLocator()
        {
            var builder = new ContainerBuilder();
            if (ConfigureUtility.ApplicationType)
            {
                var assemblylist = BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(x => x.FullName.StartsWith("Carving.Application") || x.FullName.StartsWith("Carving.Domain"));
                builder.RegisterAssemblyTypes(assemblylist.ToArray()).PropertiesAutowired().
                    AsImplementedInterfaces();
            }
            else
            {
                var assemblylist = AppDomain.CurrentDomain.GetAssemblies();
                builder.RegisterAssemblyTypes(assemblylist.ToArray()).PropertiesAutowired().
                    AsImplementedInterfaces();

            }
            //var assemblylist = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToList();AppDomain.CurrentDomain.GetAssemblies();
            _container = builder.Build();
        }

        #region Public Static Properties
        /// <summary>
        /// Gets the singleton instance of the <c>ServiceLocator</c> class.
        /// </summary>
        public static ServiceLocator Instance
        {

            get
            {
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// Gets the service instance with the given type.
        /// </summary>
        /// <typeparam name="T">The type of the service.</typeparam>
        /// <returns>The service instance.</returns>
        public T GetService<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
