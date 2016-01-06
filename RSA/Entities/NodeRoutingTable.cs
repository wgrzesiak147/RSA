using System;
using System.Collections.Generic;
using System.Linq;
using RSA.Entities;

namespace RSA
{
    /// <summary>
    /// Storing the list of routes for every node pair
    /// </summary>
    // We're sealing this class - we don't really want that to be derivated.
    // This class should be maybe more flexible and maybe it should really look like some routing table - I'll think about rewriting it.
    public sealed class NodeRoutingTable {
        private static volatile List<NodeRoutingTable> _instance;
        private static object syncRoot = new Object();
        // Some singleton to be sure that we've got our FULL Routing table (meaning all start- and endpoints) prepared only once.
        // That might be helpful in more cases than creating good routing table for a node aaaand it includes old style of reading data from files - probably refactoring should occur later
        
        // This probably should be some <int, int, List<Route>> structure rather than list of selfs
        public static List<NodeRoutingTable> RoutingTable {
            get {
                if (_instance == null) {
                    lock (syncRoot) {
                        if (_instance == null) { _instance = new List<NodeRoutingTable>(); }
                    }
                } return _instance;
            }
        }

        public int StartNodeNumber;
        public int EndNodeNumber;
        public Dictionary<int, List<Route>> RoutesToNode; 
        public List<Route> RoutesCollection;

        public NodeRoutingTable(int startNodeIndex, int endNodeIndex, List<Route> routesCollection){
            this.StartNodeNumber = startNodeIndex;
            this.EndNodeNumber = endNodeIndex;
            this.RoutesCollection = routesCollection;
            // We're adding this object to our global routing table
            RoutingTable.Add(this);
        }

        public NodeRoutingTable(int nodeIndex) {
            if (RoutingTable.Count > 0) {
                // We need only those objects that are matching our node index - we don't need other ones
                List<NodeRoutingTable> tmp = new List<NodeRoutingTable>(from route in RoutingTable
                                                                        where route.StartNodeNumber == nodeIndex
                                                                        select route);
                // Now we need to return NodeRoutingTable object WITH all those routes in some nice way
                // TODO
            }
        }

        public void PrintObjToConsole() {
            Console.WriteLine("StartNode: {0} \t|\t EndNode: {1} \t|\t NumOfRoutes: {2}",this.StartNodeNumber,this.EndNodeNumber,this.RoutesCollection.Count);
        }
    }

}
