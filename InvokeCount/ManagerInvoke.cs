using System;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeCount
{
    public class ManagerInvoke
    {
        public int TimeInvoke { get; }
        public int Count { get; private set; }
        public Action Method { get; }

        private Thread task;

        public ManagerInvoke(Action action, int timeInvoke)
        {
            TimeInvoke = timeInvoke;
            Method = action;
        }
        public void Invoke()
        {
            Method.Invoke();
            Count++;
        }
        public void Start()
        {
            task?.Abort(); 
            task = new Thread(InvokeCount);
            task.IsBackground = true;
            task.Start();
        }

        public void Stop()
        {
            task?.Abort();
        }
        public void InvokeCount()
        {
            while (true)
            {
                Thread.Sleep(TimeInvoke);
                OnCountInvoke(Count);
                Count = 0;
            }
        }

        public event EventHandler<int> CountInvoke;

        protected virtual void OnCountInvoke(int e)
        {
            CountInvoke?.Invoke(this, e);
        }
    }
}