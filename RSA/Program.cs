using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        public static string sTopologyFile =
            @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net";
        public static string sRoutesFile =
            @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d.PAT";
        public static string sSlotsFile =
            @"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\d1.spec";

        static void Main(string[] args) { 
            Topology top = new Topology();
            top.LoadTopology(sTopologyFile);

            // LoardRoutes method have to be executed before LoadSlots method!
            RoutesManager man = new RoutesManager();
            man.LoadRoutes(sRoutesFile);
            man.LoadSlots(sSlotsFile);

        }
    }
}
