using AssetPriceModule.View;
using AssetPriceModule.ViewModel;
using Infrastructure.Services;
using Infrastructure.Services.Contract;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;

namespace AssetPriceModule
{
    public class AssetPriceModule : IModule
    {
        /// <summary>
        ///     Registers the views and services.
        /// </summary>
        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IAssetPriceView, AssetPriceView>();
            _container.RegisterType<IAssetPriceViewModel, AssetPriceViewModel>();
            _container.RegisterType<IPriceFileReader, PriceFileReader>();
        }

        /*
            * 
            * 
            * 
            * 
            ****    This module has a post-build step to copy the output/dependencies to the Shell Application's assembly folder    ****
            ****    this module is then loaded at run-time from the Shell's modulemanifest.xaml file...                             ****
            * 
            * 
            * 
            * 
        */

        #region IModule Members

        /// <summary>
        ///     Gets or sets the event aggregator.
        /// </summary>
        /// <value>
        ///     The event aggregator.
        /// </value>
        [Dependency]
        public IEventAggregator EventAggregator { get; set; }

        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        /// <summary>
        ///     Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            RegisterViewsAndServices();
            if (_regionManager.Regions.ContainsRegionWithName("WorkspaceRegion"))
                _regionManager.Regions["WorkspaceRegion"].Add(_container.Resolve<IAssetPriceView>());
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="AssetPriceModule" /> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="container">The container.</param>
        public AssetPriceModule(IRegionManager regionManager, IUnityContainer container)
        {
            _container = container;
            _regionManager = regionManager;
        }

        #endregion
    }
}