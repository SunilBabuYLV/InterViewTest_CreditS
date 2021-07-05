using System;

using Microsoft.Practices.Unity;

namespace Infrastructure.UnityHelper
{
    public class StaticUnityContainer
    {
        private static IUnityContainer _container;

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>
        /// The container.
        /// </value>
        /// <exception cref="System.NullReferenceException">You must register the unity container instance in your bootstrapper</exception>
        public static IUnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    throw new NullReferenceException("You must register the unity container instance in your bootstrapper");
                }

                return _container;
            }
            set { _container = value; }
        }
    }
}
