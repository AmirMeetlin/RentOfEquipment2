using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentOfEquipment2.ClassHelper
{
    internal class AppData
    {
        public static EF.Entities Conrext { get; } = new EF.Entities();
    }
}
