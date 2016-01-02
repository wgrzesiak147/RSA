using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Entities
{
    /// <summary>
    /// Storing the Route(NodeList) and Spectrum slots(SlotsList) 
    /// </summary>
    public class Route{

        public List<int> NodeList { get; set; }
        public int[] SlotsList { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }

        public Route(List<int> nodeList){
            NodeList = nodeList;
        }
    }
}
