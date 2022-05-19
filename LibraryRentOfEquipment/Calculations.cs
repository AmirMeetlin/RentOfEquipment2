using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRentOfEquipment
{
    public static class Calculations
    {
        public static int DaysInRent(int NumberOfDayOfBegin,int NumberOfDayOfEnd)
        {
            return NumberOfDayOfEnd - NumberOfDayOfBegin + 1;
        }
        public static decimal CostOfRent(int DaysInRent,decimal Cost)
        {
            return (Cost*DaysInRent);
        }
        public static decimal Discount(int NumberOfDayOfBegin, int NumberOfDayOfEnd, decimal Cost, int NumberOfDayFactOfEnd)
        {
            return Cost * DaysInRent(NumberOfDayOfBegin, NumberOfDayFactOfEnd) - Cost * Convert.ToDecimal(0.005) * Convert.ToDecimal(DaysInRent(NumberOfDayFactOfEnd, NumberOfDayOfEnd));
        }

        public static decimal Penality(int NumberOfDayOfBegin, int NumberOfDayOfEnd, decimal Cost, int NumberOfDayFactOfEnd)
        {
            return Cost * DaysInRent(NumberOfDayOfBegin, NumberOfDayFactOfEnd)+ Cost * Convert.ToDecimal(0.01) * Convert.ToDecimal(DaysInRent(NumberOfDayFactOfEnd, NumberOfDayOfEnd));
        }
    }
}
