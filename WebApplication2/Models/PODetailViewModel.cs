using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class PODetailViewModel
    {
        public string PONO { get; set; }
        public string ITCODE { get; set; }
        public Nullable<int> QTY { get; set; }

        public virtual ItemViewModel ITEM { get; set; }
        public virtual POMasterViewModel POMASTER { get; set; }
    }
}