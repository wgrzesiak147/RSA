using System.Collections.Generic;

namespace RSA.Entities
{
    /// <summary>
    /// Storing the Route(NodeList) and Spectrum slots(SlotsList) 
    /// </summary>
    public class Route{

        public int Index { get; set; }
        public List<int> LinkList { get; set; }
        public int[] SlotsList { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Distance { get; set; } = -1;

        public List<Route> ChildsRoutes = new List<Route>();
        public List<Route> ParentsRoutes = new List<Route>();
        

        public Route(int index, List<int> linkList,int startNode,int endNode) {
            Index = index;
            LinkList = linkList;
            StartNode = startNode;
            EndNode = endNode;
        }
    }
}
