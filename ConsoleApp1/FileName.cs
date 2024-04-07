using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class FileName
    {
        //static void Print(int t)
        //{
        //    Thread.Sleep(t);     // имитация продолжительной работы
        //    Console.WriteLine("Hello METANIT.COM");
        //}

        //static async Task PrintAsync(string name, int t)
        //{
        //    Console.WriteLine("Начало метода - " + name); // выполняется синхронно
        //    await Task.Run(() => Print(t));                // выполняется асинхронно
        //    Console.WriteLine("Конец метода - " + name);
        //}

        public async Task PrintAsync(string name, int t)
        {
            Console.WriteLine("запустили поток - " + name);
            await Task.Run(() => { Console.WriteLine(name); Task.Delay(t).Wait(); });
            Console.WriteLine("конец потока - " + name);
        }


        public async Task RunAsync()
        {
            var a = PrintAsync("aaaaaaa", 10000);
            var b = PrintAsync("bbbbbbb", 5000);
            var c = PrintAsync("ccccccc", 7000);

            await b;
            await c;
            await a;

        }
    }
}
