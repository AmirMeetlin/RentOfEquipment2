using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRentOfEquipment
{
    public static class Calculations
    {
        public static decimal CostOfRent(int NumberOfDayOfBegin, int NumberOfDayOfEnd,decimal Cost)
        {
            return (Cost*(NumberOfDayOfEnd-NumberOfDayOfBegin+1));
        }
    }
}
