using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Temp
{
    internal class Qwerty
    {
        public string? str = "str";
        public static string? sstr = "sstr";

        public string? SAQ { get; set; } = "SAQ";
        public static string? SSAQ { get; set; } = "SSAQ";

        public void Print()
        { Console.WriteLine(str); }

        public static void sPrint()
        { Console.WriteLine(sstr); }

        public void SaqPrint()
        { Console.WriteLine(SAQ); }

        public static void SSaqPrint()
        { Console.WriteLine(SSAQ); }
    }
}
