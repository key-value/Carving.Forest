using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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
            var assemblylist = Assembly.GetEntryAssembly();
            builder.RegisterAssemblyTypes(assemblylist).
                AsImplementedInterfaces();
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
