using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace osVodigiWeb6x.Models      
{
    public class URLAddress
    {
        public int UrlAddressID { get; set; }
        public string UrlAddressSource { get; set; }
        public int AccountID { get; set; }  
        public string UrlAddressName { get; set; }
        public string Tags { get; set; }
        public bool IsActive { get; set; } 
    }
}