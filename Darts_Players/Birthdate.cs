using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darts_Players
{
    public class Birthdate
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public Birthdate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public override string ToString()
        {
            return $"{Year}.{Month}.{Day}";
        }
    }
}
