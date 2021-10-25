using System.Collections;
using System.Collections.Generic;

namespace ModuleHW
{
    public class ThreadSafeQueue<T> : IEnumerable<T>, IEnumerable
    {
        private readonly Queue<T> _queue;
        private readonly object _locker;

        public ThreadSafeQueue(object locker)
        {
            _queue = new Queue<T>();
            _locker = locker;
        }

        public int Count
        {
            get
            {
                lock (_locker)
                {
                    return _queue.Count;
                }
            }
        }

        public void Enqueue(T item)
        {
            lock (_locker)
            {
                _queue.Enqueue(item);
            }
        }

        public T Dequeue()
        {
            lock (_locker)
            {
                return _queue.Dequeue();
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            lock (_locker)
            {
                return _queue.GetEnumerator();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            lock (_locker)
            {
                return _queue.GetEnumerator();
            }
        }
    }
}
