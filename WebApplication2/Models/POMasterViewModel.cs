using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class POMasterViewModel
    {
        public string PONO { get; set; }
        public Nullable<System.DateTime> PODATE { get; set; }
        public string SUPLNO { get; set; }
        public virtual SupplierViewModel SUPPLIER { get; set; }
    }
}