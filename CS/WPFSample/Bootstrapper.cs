using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using Infrastructure.UnityHelper;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace WPFSample
{
    public class Bootstrapper : CustomBootstrapper
    {
        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>
        /// The shell of the application.
        /// </returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject" />, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper" /> will attach the default <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager" /> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty" /> attached property
        /// in order to be able to add regions by using the <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty" />
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Shell)Shell;

            if (Application.Current.MainWindow != null)
            {
                Application.Current.MainWindow.Show();
            }
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Infrastructure.UnityHelper.StaticUnityContainer.Container = Container;
        }

        /// <summary>
        /// Creates the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog" /> used by Prism.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {

            var fsReader = new FileStream(@".\ModuleCatalog.xaml", FileMode.Open);
            ModuleCatalog modules = Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(fsReader);
            
            return modules;
        }
    }
}
