using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiPlayer.Data
{
    class WebShowURLAddressXRef
    {
        public int WebShowURLAddressXRefID { get; set; }
        public int WebShowID { get; set; } 
        public int URLAddressID { get; set; } 
        public int PlayOrder { get; set; }
        public int Zoom { get; set; }
    }
}
