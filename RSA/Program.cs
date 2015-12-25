using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    class Program
    {
        static void Main(string[] args)
        { 

            Topology top = new Topology();
            top.LoadTopology(@"C:\Users\wgrzesiak147\Downloads\RSA_any_uni_dane\RSA any uni dane\DT14 1.75 Tbps\dd.net");
        }
    }
}
