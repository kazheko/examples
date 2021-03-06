﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Examples.AuctionApi.Installers
{
    public class StoresInstallers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                .FromThisAssembly()
                .InNamespace("Examples.AuctionApi.Infrastructure")
                .Configure(c=>c.OnlyNewServices())
                .WithServiceAllInterfaces());
        }
    }
}
