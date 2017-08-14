using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nancy.ContainerSharing
{
    public class Bootstrapper: Nancy.Bootstrappers.StructureMap.StructureMapNancyBootstrapper
    {
        private readonly IContainer _container;
        public Bootstrapper(IContainer container)
        {
            _container = container;
        }
        protected override IContainer GetApplicationContainer()
        {
            return _container;
        }
    }
}
