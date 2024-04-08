namespace Temp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileName fileName = new FileName();
            fileName.RunAsync();

            Thread.Sleep(7000);

        }
    }
}
