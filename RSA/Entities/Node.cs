using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Entities {
    class Node {
        private static int count = 0;
        // Neighboring nodes
        private List<int> neighborList;
        // <node_index, list_of_available_routes_from_that_node>
        // private Dictionary<int, List<Route>> availableRoutes; // or we can use "routing table" principle
        private NodeRoutingTable routingTable;
        public int Index { get; set; } = 0;
        public Node(List<int> _neighborList) {
            // Unsafe for multithread - TOFIX
            // Implementation not yet finished. - TODO
            Index = count;
            neighborList = new List<int>(_neighborList);
            routingTable = new NodeRoutingTable(this.Index);
            count++;
        }
        public Node() {
            // Unsafe for multithread - TOFIX
            // Implementation not yet finished. - TODO
            Index = count;
            routingTable = new NodeRoutingTable(this.Index);
            count++;

        }
        public List<int> ReturnAllNeigbors() { return neighborList; }
        static public int ReturnNodeCount() { return count;}
    }
}
