using System;

namespace RSA
{
    class Program
    {
        static void Main(string[] args) {

            string directoryPath = AppDomain.CurrentDomain.BaseDirectory;

            ////  string sTopologyFile =
            // @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net";
            string sTopologyFile =directoryPath + 
             @"Dane\dd.net";
            string sRoutesFile =
                @"Dane\d.PAT";
            string sSlotsFile =
                @"Dane\d1.spec";
            string sRequestFile =
                @"Dane\51.dem";

            TopologyManager top = new TopologyManager();
            top.LoadTopology(sTopologyFile);

            // LoadRoutes method have to be executed before LoadSlots method!
            RoutesManager man = new RoutesManager();
            man.LoadRoutes(sRoutesFile);
            man.LoadSlots(sSlotsFile);
            man.LoadWeights(top.CurrentTopology);
            RequestManager req = new RequestManager();
            req.LoadRequests(sRequestFile);




        }
    }
}
