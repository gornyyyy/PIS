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
            
            DateTime date = DateTime.ParseExact(parts[0], "yyyy.MM.dd", null);
            double height = double.Parse(parts[1]);
            int value = int.Parse(parts[2]);

            return new Pressure(date, height, value);
            
        }

        public static LiquidPressure ParseLiquidPressure(string text)
        {
            string[] parts = text.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            DateTime date = DateTime.ParseExact(parts[0], "yyyy.MM.dd", null);
            double height = double.Parse(parts[1]);
            int value = int.Parse(parts[2]);
            string liquidtype = parts[3];
            double volume = double.Parse(parts[4]);

            return new LiquidPressure(date, height, value, liquidtype, volume);
        }

        public static GasPressure ParseGasPressure(string text)
        {
            string[] parts = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            DateTime date = DateTime.ParseExact(parts[0], "yyyy.MM.dd", null);
            double height = double.Parse(parts[1]);
            int value = int.Parse(parts[2]);
            string gastype = parts[3];
            bool isinert = false;
            if (parts[4] == "Инертный")
            {
                isinert = true;
            }
            
            return new GasPressure(date, height, value, gastype, isinert);  
        }


        public static Pressure ParseVariousPressure(string text)
        {
            string[] parts = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 3)
            {
                return ParsePressure(text);
            }
            else if (parts.Length == 5)
            {
                if (double.TryParse(parts[4], out _))
                {
                    return ParseLiquidPressure(text);
                }
                else
                {
                    return ParseGasPressure(text);
                }
            }

            return null;
        }

        static void Main(string[] args)
        {
            string text1 = "2023.02.28 1020,75 1";
            Pressure pressure1 = ParsePressure(text1);
            Console.WriteLine(pressure1);

            string[] lines = File.ReadAllLines("C:\\Users\\gorno\\OneDrive\\Рабочий стол\\student\\3 семестр\\ПИС\\PIS\\app1\\Pressures.txt", Encoding.GetEncoding(1251));

            foreach (string line in lines)
            {
                Pressure pressure = ParseVariousPressure(line);
                Console.WriteLine(pressure);
            }

        }
    }
}
