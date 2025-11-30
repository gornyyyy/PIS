using System;
using System.Xml.Schema;

namespace app1
{
    public static class PressureParser
    {
        private const string DateFormat = "yyyy.MM.dd";
        private const string InertGasIndicator = "Инертный";

        public static string[] SplitText(string text)
        {
            if (text == null) return null;
            return text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static (DateTime date, double height, int value, string device) ParseBaseParameters(string[] parts)
        {
            if (parts.Length < 4 || parts == null)
                throw new ArgumentException("недостаточно информации в строке");

            DateTime date = DateTime.ParseExact(parts[0], DateFormat, null);
            double height = double.Parse(parts[1].Replace('.', ','));
            int value = int.Parse(parts[2]);
            string device = parts[3];

            return (date, height, value, device);

        }

        private static Pressure ParsePressure(string text)
        {
            try
            {
                string[] parts = SplitText(text);
                var (date, height, value, device) = ParseBaseParameters(parts);

                return new Pressure(date, height, value, device);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                Console.WriteLine($"Ошибка парсинга: {ex.Message}");
                return null;
            }
        }

        private static LiquidPressure ParseLiquidPressure(string text)
        {
            try
            {
                string[] parts = SplitText(text);
                var (date, height, value, device) = ParseBaseParameters(parts);

                string liquidType = parts[4];
                double volume = double.Parse(parts[5].Replace('.', ','));

                return new LiquidPressure(date, height, value, liquidType, volume, device);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                Console.WriteLine($"Ошибка парсинга: {ex.Message}");
                return null;
            }
        }

        private static GasPressure ParseGasPressure(string text)
        {
            try
            {
                string[] parts = SplitText(text);
                var (date, height, value, device) = ParseBaseParameters(parts);

                string gasType = parts[4];
                bool isInert = parts[5] == InertGasIndicator;

                return new GasPressure(date, height, value, gasType, isInert, device);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                Console.WriteLine($"Ошибка парсинга: {ex.Message}");
                return null;
            }
        }

        private static AtmosphericPressure ParseAtmosphericPressure(string text)
        {
            try
            {
                string[] parts = SplitText(text);
                var (date, height, value, device) = ParseBaseParameters(parts);

                double temperature = double.Parse(parts[4].Replace('.',','));

                return new AtmosphericPressure(date, height, value, device, temperature);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                Console.WriteLine($"Ошибка парсинга: {ex.Message}");
                return null;
            }
        }

        private static string DeterminePressureType(string[] parts)
        {
            if (parts.Length == 4)
            {
                return "Base";
            }
            else if (parts.Length == 5)
            {
                return double.TryParse(parts[4], out _) ?
                    "Atmospheric" : null;
            }

            else if (parts.Length == 6)
            {
                return double.TryParse(parts[5], out _) ?
                "Liquid" : "Gas";
            }

            return null;
        }

        public static Pressure ParseVariousPressure(string text)
        {
            try
            {
                if (text == null) { return null; }

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
            catch (Exception ex) when (ex is ArgumentException || ex is FormatException)
            {
                Console.WriteLine($"Ошибка парсинга: {ex.Message}");
                return null;
            }
        }
    }
}
