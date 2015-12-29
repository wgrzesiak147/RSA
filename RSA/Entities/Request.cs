using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class Request{

        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Capacity { get; set; }

        public Request(int startNode, int endNode, int capacity)
        {
            StartNode = startNode;
            EndNode = endNode;
            Capacity = capacity;
        }
    }
}
