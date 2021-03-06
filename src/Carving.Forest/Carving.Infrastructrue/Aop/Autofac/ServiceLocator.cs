﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Compilation;
using Autofac;
using Autofac.Extras.DynamicProxy2;

namespace Carving.Infrastructrue.Aop.Autofac
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
            var nameSpaces = new List<string>() { "Carving.Application", "Carving.Domain.Repository.EF", "Carving.Domain.Core", "Carving.Domain.Events.Handles" };

            foreach (var nameSpace in nameSpaces)
            {
                var interfaceTypes = AppDomain.CurrentDomain.Load(nameSpace).GetTypes().Where(x => x.GetCustomAttributes(typeof(InjectionAttribute), false).Any()).ToArray();


                foreach (var injectionType in interfaceTypes)
                {
                    var unityInjectionAttribute = injectionType.GetCustomAttributes(typeof(InjectionAttribute), false) as InjectionAttribute[];
                    if (unityInjectionAttribute == null)
                    {
                        continue;
                    }
                    foreach (var customAttributeData in unityInjectionAttribute)
                    {
                        var classBody = customAttributeData.InterfaceName;
                        if (classBody != null)
                        {
                            builder.RegisterTypes(injectionType).As(classBody).PropertiesAutowired().InstancePerLifetimeScope().EnableClassInterceptors().InterceptedBy(typeof(CallLogger));
                        }
                    }
                }
            }
            builder.Register(x => new CallLogger()).SingleInstance();

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

        public IEnumerable<T> ResolveAll<T>()
        {
            return new List<T>() { _container.Resolve<T>() };
        }
    }
}
