using System;

namespace Infrastructure.Events
{
    public sealed class EventArgs<TData> : EventArgs
    {
        private readonly TData _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgs{TData}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public EventArgs(TData value)
        {
            _value = value;
        }
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public TData Value
        {
            get { return _value; }
        }
    }
}
