using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AtlasCopco.Integration.Maze;
using Autofac;

namespace AtlasCopco.App.Emulator.Console
{
    internal static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            // Make sure process paths are sane...
            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

            //  begin setup of autofac >>

            // 1. Scan for assemblies containing dlls that implement IMazeIntegration in the bin folder
            var assemblies = new List<Assembly>();
            assemblies.AddRange(
                Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll", SearchOption.AllDirectories)
                    .Select(Assembly.LoadFrom)
            );

            //TODO
            // Fix to handle multiple implementations/Load one implementation
            foreach (var assembly in assemblies)
            {
                builder.RegisterAssemblyTypes(assembly)
                    .Where(t => t.GetInterfaces()
                        .Any(i => i.IsAssignableFrom(typeof(IMazeIntegration))))
                    .AsImplementedInterfaces()
                    .InstancePerRequest();
            }
            builder.RegisterType<ASCIIConsole>().As<IEmulator>();

            return builder.Build();
        }
    }
}