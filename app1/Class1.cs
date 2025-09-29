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

        public Pressure(DateTime date, double height, int value)
        {
            Date = date;
            Height = height;
            Value = value;
        }

        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height})";
        }
    }

    public class LiquidPressure: Pressure  
    {
        public string LiquidType {  get; set; }
        public double Density { get; set; }

        public LiquidPressure(DateTime date, double height, int value, string liquidtype, double density) : base(date, height, value)
        {
            LiquidType = liquidtype;
            Density = density;
        }
        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}, жидкость: {LiquidType}, плотность жидкости)";
        }
    }

    public class GasPressure : Pressure
    {
        public string GasType { get; set; }
        public bool IsInert {  get; set; }
        public GasPressure(DateTime date, double height, int value, string gastype, bool isinert) : base(date, height, value)
        {
            GasType = gastype;
            IsInert = isinert;
        }
        public override string ToString()
        {
            return $"Давление: {Value} Па (дата: {Date:yyyy.MM.dd}, высота: {Height}, газ: {GasType}, инертный: {IsInert})";
        }
    }

}
