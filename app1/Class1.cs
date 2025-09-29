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
}
