using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Prism.Events
{
    public static class CustomEventAggregator
    {
        private static IEventAggregator _aggregator;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static IEventAggregator GetInstance()
        {
            lock (typeof(CustomEventAggregator))
            {
                return _aggregator ?? (_aggregator = new EventAggregator());
            }
        }
    }
}
