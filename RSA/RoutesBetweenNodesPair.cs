using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class RoutesBetweenNodesPair
    {

        public int StartNodeNumber;
        public int EndNodeNumber;
        public List<Route> RoutesCollection;

        public RoutesBetweenNodesPair(int startNodeNumber, int endNodeNumber, List<Route> routesCollection)
        {
            StartNodeNumber = startNodeNumber;
            EndNodeNumber = endNodeNumber;
            RoutesCollection = routesCollection;
        }
    }

    public class Route
    {
        public List<int> NodeList;

        public Route(List<int> nodeList)
        {
            NodeList = nodeList;
        }
    }
}
