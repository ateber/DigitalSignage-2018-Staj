 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiPlayer.Data
{
    public class URLAddress
    {
        public int URLAddressID { get; set; }
        public string URLAddressSource{ get; set; }
        public string URLAddressName { get; set; }
        public int AccountID { get; set; } 
        public string Tags { get; set; }
        public bool IsActive { get; set; } 
    }
}
