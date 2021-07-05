using Infrastructure.Prism.Events;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.UnityExtensions;

using Microsoft.Practices.Unity;

namespace Infrastructure.UnityHelper
{
    public abstract class CustomBootstrapper : UnityBootstrapper
    {
        /// <summary>
        /// Configures the <see cref="T:Microsoft.Practices.Unity.IUnityContainer" />. May be overwritten in a derived class to add specific
        /// type mappings required by the application.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            //Replace the default IEventAggregator by the one used in exception handling with postsharp
            Container.RegisterInstance<IEventAggregator>(CustomEventAggregator.GetInstance());
        }
    }
}
