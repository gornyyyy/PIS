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

        private static (DateTime date, double height, int value, string device) ParseBaseParameters(string[] parts)
        {
            DateTime date = DateTime.ParseExact(parts[0], DateFormat, null);
            double height = double.Parse(parts[1]);
            int value = int.Parse(parts[2]);
            string device = parts[3];

            return (date, height, value, device);
        }

        private static Pressure ParsePressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value, device) = ParseBaseParameters(parts);

            return new Pressure(date, height, value, device);
        }

        private static LiquidPressure ParseLiquidPressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value, device) = ParseBaseParameters(parts);

            string liquidType = parts[4];
            double volume = double.Parse(parts[5]);

            return new LiquidPressure(date, height, value, liquidType, volume, device);
        }

        private static GasPressure ParseGasPressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value, device) = ParseBaseParameters(parts);

            string gasType = parts[4];
            bool isInert = parts[5] == InertGasIndicator;

            return new GasPressure(date, height, value, gasType, isInert, device);
        }

        private static AtmosphericPressure ParseAtmosphericPressure(string text)
        {
            string[] parts = SplitText(text);
            var (date, height, value, device) = ParseBaseParameters(parts);

            double temperature = double.Parse(parts[4]);

            return new AtmosphericPressure(date, height, value, device, temperature);
        }

        private static string DeterminePressureType(string[] parts)
        {
            if (parts.Length == 4)
            {
                return "Base";
            }
            else if (parts.Length == 5)
            {
                return "Atmospheric";
            }

            else if (parts.Length == 6)
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
            case "Atmospheric":
                return ParseAtmosphericPressure(text);
            default:
                return null;
            }
            
        }
    }
}
