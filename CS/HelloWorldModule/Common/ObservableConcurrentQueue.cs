using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;

namespace AssetPriceModule.Common
{
    public class ObservableConcurrentQueue<T> : INotifyCollectionChanged, IEnumerable<T>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        private readonly ConcurrentQueue<T> _queue = new ConcurrentQueue<T>();

        public void Enqueue(T item)
        {
            _queue.Enqueue(item);
            if (CollectionChanged != null)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    CollectionChanged(this,
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Add, item));
                });
            }
        }

        public T Dequeue()
        {
            _queue.TryDequeue(out var item);

            if (CollectionChanged != null)

            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    CollectionChanged(this,
                        new NotifyCollectionChangedEventArgs(
                            NotifyCollectionChangedAction.Remove, item,0));
                });
              
            }
            return item;
        }

        public IEnumerator<T> GetEnumerator()
            {
                return _queue.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
        {
                return GetEnumerator();
            }

        public int Count
        {
            get
            {
                return _queue.Count;
            }
        }
    }
}