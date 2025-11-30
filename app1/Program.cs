using System;
using System.IO;
using System.Text;

namespace app1
{
    internal class Program
    {

        private const string FilePath = @"C:\Users\Ganiev Egor\Desktop\УЧЕБА(\3 СЕМЕСТР\пис\PIS\app1\Pressures.txt";

        static void Main(string[] args)
        {

            string text1 = null;
            Pressure pressure1 = PressureParser.ParseVariousPressure(text1);
            Console.WriteLine(pressure1);

            string[] lines = File.ReadAllLines(FilePath, Encoding.GetEncoding(1251));

            foreach (string line in lines)
            {
                Pressure pressure = PressureParser.ParseVariousPressure(line);
                if (pressure != null)
                {
                    Console.WriteLine(pressure);
                }

            }
            Console.WriteLine("Пример входящих данных \n" +
                "дата".PadRight(11) + "высота(double)" + "   значение(int)" + "   устройство\n" +
                "2025.10.10 123,123          33              барометр");
            while (true)
            {
                string s = Console.ReadLine();
                Pressure s1 = PressureParser.ParseVariousPressure(s);
                Console.WriteLine(s1);
            }
            

        }
    }
}
