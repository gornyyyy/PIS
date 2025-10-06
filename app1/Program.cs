using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    internal class Program
    {
        
        private const string FilePath = @"C:\Users\gorno\OneDrive\Рабочий стол\student\3 семестр\ПИС\PIS\app1\Pressures.txt";

        static void Main(string[] args)
        {
            string text1 = "2023.02.28 1020,75 1";
            Pressure pressure1 = PressureParser.ParseVariousPressure(text1);
            Console.WriteLine(pressure1);

            string[] lines = File.ReadAllLines(FilePath, Encoding.GetEncoding(1251));

            foreach (string line in lines)
            {
                Pressure pressure = PressureParser.ParseVariousPressure(line);
                Console.WriteLine(pressure);
            }

        }
    }
}
