using System;

namespace ModuleHW
{
    public class Subscriber
    {
        private readonly int _id;
        private readonly ThreadSafeQueue<int> _queue;
        private readonly object _locker;

        public Subscriber(int id, ThreadSafeQueue<int> queue, object locker)
        {
            _id = id;
            _queue = queue;
            _locker = locker;
        }

        public void DoDequeue()
        {
            try
            {
                if (_queue.Count == 0)
                {
                    lock (_locker)
                    {
                        // Console.WriteLine($"SUB: The queue is empty!");
                        return;
                    }
                }

                lock (_locker)
                {
                    Console.WriteLine($"<= SUB[{_id}]: Item dequeued: \"{_queue.Dequeue()}\"!");
                    Console.WriteLine(string.Empty);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
