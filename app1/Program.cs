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
        public static Pressure ParsePressure(string text)
        {
            string[] parts = text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0)
            {
                DateTime date = DateTime.ParseExact(parts[0], "yyyy.MM.dd", null);
                double height = double.Parse(parts[1]);
                int value = int.Parse(parts[2]);

                return new Pressure(date, height, value);
            }
            return null;
        }

        static void Main(string[] args)
        {
            string text1 = "2023.02.28 1020,75 1";
            Pressure pressure1 = ParsePressure(text1);
            Console.WriteLine(pressure1);

            string[] lines = File.ReadAllLines("C:\\Users\\gorno\\OneDrive\\Рабочий стол\\student\\3 семестр\\ПИС\\PIS\\app1\\Pressures.txt");

            foreach (string line in lines)
            {
                Pressure pressure = ParsePressure(line);
                Console.WriteLine(pressure);
            }

        }
    }
}
