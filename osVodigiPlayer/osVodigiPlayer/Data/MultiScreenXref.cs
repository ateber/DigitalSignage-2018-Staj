using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osVodigiPlayer.Data
{
    public class MultiScreenXref
    {
        public int MultiScreenXrefID { get; set; }
        public int MultiScreenID { get; set; }
        public int ScreenID { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Height { get; set; }
        public double Width { get; set; } 
    }
}
