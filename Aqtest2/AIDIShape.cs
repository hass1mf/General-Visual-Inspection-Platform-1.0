using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AqAidi
{
    public class Contours
   {
        public string x { get; set; }
        public string y { get; set; }
    }
  public   class AIDIShape
    {
        public string area { get; set; }
        public List<Contours> contours { get; set; }
        public string cx { get; set; }
        public string cy { get; set; }
        public string height { get; set; }
        public string score { get; set; }
        public string type { get; set; }
        public string type_name { get; set; }
        public string width { get; set; }    }
}
