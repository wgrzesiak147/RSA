namespace RSA
{
    public class Request
    {

        public int Id { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Capacity { get; set; }

        public Request(int startNode, int endNode, int capacity){
            StartNode = startNode;
            EndNode = endNode;
            Capacity = capacity;
        }
    }
}
