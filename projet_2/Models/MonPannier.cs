using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace projet_2.Models
{
    public class MonPannier
    {
        public string Designation { get; set; }
        public string PrixU { get; set; }
        public string photo { get; set; }
        public string DateCmd { get; set; }
        public Nullable<int> QteArticle { get; set; }
    }
}