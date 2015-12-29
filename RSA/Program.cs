using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args) {


            
            string sTopologyFile =
                @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net";
            string sRoutesFile =
                @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d.PAT";
            string sSlotsFile =
                @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d1.spec";
            string sRequestFile =
                @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\51.dem";

            TopologyManager top = new TopologyManager();
            top.LoadTopology(sTopologyFile);

            // LoadRoutes method have to be executed before LoadSlots method!
            RoutesManager man = new RoutesManager();
            man.LoadRoutes(sRoutesFile);
            man.LoadSlots(sSlotsFile);

            RequestManager req = new RequestManager();
            req.LoadRequests(sRequestFile);




        }
    }
}
