using System;
using Autofac;

namespace AutoCompleteService.Native.Global
{
    public static class IoC
    {
        private static IContainer _container;

        public static void UpdateRegistrations(Action<ContainerBuilder> action)
        {
            var containerBuilder = new ContainerBuilder();
            action(containerBuilder);
            containerBuilder.Update(Container);
        }

        public static IContainer Container
        {
            get
            {
                if (_container == null)
                {
                    var builder = new ContainerBuilder();
                    _container = builder.Build();
                }
                return _container;
            }
        }
    }
}
