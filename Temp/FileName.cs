using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    internal class FileName
    {
        public async Task PrintNameAsync(string name, int t)
        {
            Console.WriteLine("start: " + name);
            await Task.Delay(t);
            Console.WriteLine("finish: " + name);
        }

        public async Task<string> Print(string name, int t)
        {
            Console.WriteLine("start: " + name);
            await Task.Delay(t);
            Console.WriteLine(".......");
            //var today = await Task.FromResult("finish: " + name);
            //return today;
            return await Task.Run(() => { return ("finish::: " + name); });
        }


        public void QQQ(string name, int t)
        {
            new Thread(() => { Console.WriteLine(Print(name, t).Result); }).Start();
        }




        public string PrintName(string name, int t)
        {
            Console.WriteLine("start: " + name);

            Console.WriteLine("finish: " + name);
            return name;
        }




        public void RunAsync()
        {
            //Print("Tom", 4000);
            //Print("Bob", 2000);
            //Print("Sam", 6000);

            QQQ("Tom", 4000);
            _ = Print("Bob", 2000);
            _ = Print("Sam", 6000);







        }
    }
}
