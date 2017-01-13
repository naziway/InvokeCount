using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvokeCount
{
    class Program
    {
        public static ManagerInvoke managerInvoke;
        static void Main(string[] args)
        {
            managerInvoke = new ManagerInvoke(() =>
            {
                Thread.Sleep(800);
                Console.WriteLine("!");
            }, 1000);
            managerInvoke.CountInvoke += ManagerInvokeCountInvoke;
            managerInvoke.Start();
            var task = new Timer(Dub);
            task.Change(25, 10);
            Console.ReadKey();
            managerInvoke.Stop();
            task.Dispose();
            //   task.InitializeLifetimeService();
        }

        private static void Dub(object state)
        {
            managerInvoke.Invoke();
        }

        private static void ManagerInvokeCountInvoke(object sender, int e)
        {
            Console.WriteLine($"Count invoke = {e}");
        }
    }
}
