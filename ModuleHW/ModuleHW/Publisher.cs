using System;

namespace ModuleHW
{
    public class Publisher
    {
        private readonly int _id;
        private readonly ThreadSafeQueue<int> _queue;
        private readonly object _locker;

        public Publisher(int id, ThreadSafeQueue<int> queue, object locker)
        {
            _id = id;
            _queue = queue;
            _locker = locker;
        }

        public void DoEnqueue()
        {
            try
            {
                lock (_locker)
                {
                    var num = RandomHelper.GetRandomNumber();
                    _queue.Enqueue(num);
                    Console.WriteLine($"=> PUB[{_id}]: Item enqueued: \"{num}\"!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
