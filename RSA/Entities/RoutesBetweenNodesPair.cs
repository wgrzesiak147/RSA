using System.Collections.Generic;

namespace RSA
{
    /// <summary>
    /// Storing the list of routes for every node pair
    /// </summary>
    public class RoutesBetweenNodesPair{

        public int StartNodeNumber;
        public int EndNodeNumber;
        public List<Route> RoutesCollection;

        public RoutesBetweenNodesPair(int startNodeNumber, int endNodeNumber, List<Route> routesCollection){
            StartNodeNumber = startNodeNumber;
            EndNodeNumber = endNodeNumber;
            RoutesCollection = routesCollection;
        }
    }

    /// <summary>
    /// Storing the Route(NodeList) and Spectrum slots(SlotsList) 
    /// </summary>
    public class Route{

        public List<int> NodeList;
        public int[] SlotsList; 

        public Route(List<int> nodeList){
            NodeList = nodeList;
        }
    }

}
