namespace RSA.Entities
{
    public class Link
    {
        public int Index { get; set; }
        public int StartNode { get; set; }
        public int EndNode { get; set; }
        public int Distance { get; set; }
        public int FreeSlotsNumber { get; set; }
        public int TakenSlotsNumber { get; set; }
        public int SlotsArray { get; set; }

        public Link(int id, int startNode, int endNode, int distance)
        {
            Index = id;
            StartNode = startNode;
            EndNode = endNode;
            Distance = distance;
            
        }

        
    
    }
}
