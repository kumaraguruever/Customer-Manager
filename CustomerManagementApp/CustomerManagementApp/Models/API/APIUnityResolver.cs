using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Unity;

namespace CustomerManagementApp.Models.API
{
    public class APIUnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public APIUnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new APIUnityResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

    }
}