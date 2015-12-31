using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA.Entities
{
    public class Connection
    {
        public int Id;
        public int StartNode;
        public int EndNode;
        public int Weight;

        public Connection(int id, int startNode, int endNode, int weight)
        {
            Id = id;
            StartNode = startNode;
            EndNode = endNode;
            Weight = weight;
        }
    }
}
