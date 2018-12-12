using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class ItemViewModel
    {
        public string ITCODE { get; set; }
        public string ITDESC { get; set; }
        public Nullable<decimal> ITRATE { get; set; }
    }
}