using System.Collections.Generic;
using RSA.Entities;

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

}
