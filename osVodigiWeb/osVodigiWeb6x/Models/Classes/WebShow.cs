using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace osVodigiWeb6x.Models
{
    public class WebShow
    {
        public int WebShowID { get; set; }
        public int AccountID { get; set; }
        public string WebShowName { get; set; }
        public string Tags { get; set; } 
        public int IntervalInSecs { get; set; }
        public bool IsActive { get; set; }
    }
}