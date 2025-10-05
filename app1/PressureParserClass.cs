using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    static class PressureParser
    {
        private const string DateFormat = "yyyy.MM.dd";
        private const string InertGasIndicator = "Инертный";

        public static string[] SplitText(string text)
        {
            return text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static (DateTime date, double height, int value) ParseBaseParameters(string[] parts)
        {
            DateTime date = DateTime.ParseExact(parts[0], DateFormat, null);
            double height = double.Parse(parts[1]);
            int value = int.Parse(parts[2]);

            return (date, height, value);
        }

        private static Pressure ParsePressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value) = ParseBaseParameters(parts);

            return new Pressure(date, height, value);
        }

        private static LiquidPressure ParseLiquidPressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value) = ParseBaseParameters(parts);

            string liquidType = parts[3];
            double volume = double.Parse(parts[4]);

            return new LiquidPressure(date, height, value, liquidType, volume);
        }

        private static GasPressure ParseGasPressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value) = ParseBaseParameters(parts);

            string gasType = parts[3];
            bool isInert = parts[4] == InertGasIndicator;

            return new GasPressure(date, height, value, gasType, isInert);
        }

        private static string DeterminePressureType(string[] parts)
        {
            if (parts.Length == 3)
            {
                return "Base";
            }
            else if (parts.Length == 5)
            {
                return double.TryParse(parts[4], out _) ?
                    "Liquid" : "Gas";
            }

            return "Unknown";
        }

        public static Pressure ParseVariousPressure(string text)
        {
            string[] parts = SplitText(text);
            string pressureType = DeterminePressureType(parts);

            switch (pressureType)
            {
                case "Base":
                    return ParsePressure(text);
                case "Liquid":
                    return ParseLiquidPressure(text);
                case "Gas":
                    return ParseGasPressure(text);
            }
            return null;
        }
    }
}
