using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app1
{
    public class Pressure
    {
        public DateTime Date { get; set; }
        public double Height { get; set; }
        public int Value { get; set; }
        public string Device { get; set; }

        public Pressure(DateTime date, double height, int value, string device)
        {
            Date = date;
            Height = height;
            Value = value;
            Device = device;
        }

        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}, устройство: {Device})";
        }
    }

    public class LiquidPressure: Pressure  
    {
        public string LiquidType {  get; set; }
        public double Volume { get; set; }

        public LiquidPressure(DateTime date, double height, int value, string liquidtype, double volume, string device) : base(date, height, value, device)
        {
            LiquidType = liquidtype;
            Volume = volume;
        }
        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}, устройство: {Device};" +
                $" жидкость: {LiquidType}, объем жидкости: {Volume})";
        }
    }

    public class GasPressure : Pressure
    {
        public string GasType { get; set; }
        public bool IsInert {  get; set; }
        public GasPressure(DateTime date, double height, int value, string gastype, bool isinert, string device) : base(date, height, value, device)
        {
            GasType = gastype;
            IsInert = isinert;
        }
        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}, устройство: {Device};" +
                $" газ: {GasType}, инертный: {IsInert})";
        }
    }

    public class AtmosphericPressure : Pressure
    {
        public double Temperature { get; set; }
        public AtmosphericPressure(DateTime date, double height, int value, string device, double temperature) : base (date, height, value, device)
        {
            Temperature = temperature;
        }

        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}; устройство: {Device}" +
                $" температура воздуха: {Temperature}";
        }
    }
}
