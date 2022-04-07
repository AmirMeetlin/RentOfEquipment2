using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentOfEquipment2.ClassHelper
{
    class DataTransmission
    {
        public static EF.Client GetClient { get; set; }
        public static EF.Employee GetEmployee { get; set; }
        public static EF.Product GetEquipment { get; set; }
    }
}
