using System;
using System.Threading;

namespace ModuleHW
{
    public class Starter
    {
        private readonly ThreadSafeQueue<int> _numbers;
        private readonly object _locker;
        private readonly Semaphore _semaphore;
        private int _count;

        public Starter()
        {
            _locker = new object();
            _numbers = new ThreadSafeQueue<int>(_locker);
            _semaphore = new Semaphore(1, 1);
            _count = 10;
        }

        public void Run()
        {
            var threadPublisher1 = new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = "threadPublisher1";

                    var publisher1 = new Publisher(1, _numbers, _locker);

                    while (true)
                    {
                        if (_count > 0)
                        {
                            _semaphore.WaitOne();
                            publisher1.DoEnqueue();
                            _count--;
                            Thread.Sleep(800);
                            _semaphore.Release();
                        }
                        else
                        {
                            _semaphore.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            });

            var threadPublisher2 = new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = "threadPublisher2";

                    var publisher2 = new Publisher(2, _numbers, _locker);

                    while (true)
                    {
                        if (_count > 0)
                        {
                            _semaphore.WaitOne();
                            publisher2.DoEnqueue();
                            _count--;
                            Thread.Sleep(800);
                            _semaphore.Release();
                        }
                        else
                        {
                            _semaphore.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            });

            var threadSubscriber1 = new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.Name = "threadSubscriber1";

                    var subscriber1 = new Subscriber(1, _numbers, _locker);

                    while (true)
                    {
                        if (_count >= 0)
                        {
                            subscriber1.DoDequeue();
                            Thread.Sleep(50);
                        }
                        else
                        {
                            _semaphore.Close();
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                }
            });

            threadSubscriber1.Start();
            threadPublisher2.Start();
            threadPublisher1.Start();
        }
    }
}
