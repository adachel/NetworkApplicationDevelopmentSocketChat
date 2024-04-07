using System.Threading.Channels;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            FileName fileName = new FileName();
            _ = fileName.RunAsync();
            Thread.Sleep(12000);
        }
    }
}
