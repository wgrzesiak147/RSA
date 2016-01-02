using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Entities
{
    public class Connection
    {
        public int Id { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Weight { get; set; }
        public int FreeSlotsNumber { get; set; }
        public int TakenSlotsNumber { get; set; }
        public int SlotsArray { get; set; }

        public Connection(int id, int startNode, int endNode, int weight)
        {
            Id = id;
            StartNode = startNode;
            EndNode = endNode;
            Weight = weight;
        }
    }
}
