//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentOfEquipment2.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rent
    {
        public int ID { get; set; }
        public int IDClient { get; set; }
        public int IDProduct { get; set; }
        public int IDEmployee { get; set; }
        public System.DateTime TimeRent { get; set; }
        public System.DateTime TimeRentEnd { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<decimal> TotalCost { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Product Product { get; set; }
    }
}
