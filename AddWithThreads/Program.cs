using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AddWithThreads
{
    class Program
    {
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);
        static void Main(string[] args)
        {
            Console.WriteLine("Adding with Thread objects");
            Console.WriteLine($"Id of thread in Main(): {Thread.CurrentThread.ManagedThreadId}");
            //Создать объект AddParams для передачи вторичному потоку
            AddParams ap = new AddParams(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);
            //Ожидать пока не поступит уведомление
            waitHandle.WaitOne();
            Console.WriteLine("Other thread is done!");
            Console.ReadLine();
        }

        static void Add(object data)
        {
            if (data is AddParams)
            {
                Console.WriteLine($"Id is thread in Add(): {Thread.CurrentThread.ManagedThreadId}");
                AddParams ap = (AddParams)data;
                Console.WriteLine($"{ap.a} + {ap.b} is {ap.a + ap.b}");
                waitHandle.Set(); //Сообщить другому потоку о том, что работа завершена!
            }
        }
    }
}
