using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA {
    public class RoutesManager {
        public int RoutesQuantity;
        public List<RoutesBetweenNodesPair> RoutesBetweenNodesPairsCollection = new List<RoutesBetweenNodesPair>();
       
        /// <summary>
        /// Loading routes from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadRoutes(string path){
            int counter = 0;
            int endNodeNumber = 0;
            string line;
            try {
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null) {
                    if (counter == 0) {
                        Int32.TryParse(line, out RoutesQuantity);
                    }
                    else {
                        List<int> coll = line.Split(' ').Select(Int32.Parse).ToList();

                        Route currentRoute = CalculateRouteFromBinary(coll);

                        var routesForNodes =
                            RoutesBetweenNodesPairsCollection.FirstOrDefault(
                                x => x.StartNodeNumber == 0 && x.EndNodeNumber == endNodeNumber);

                        if (routesForNodes != null) {
                            routesForNodes.RoutesCollection.Add(currentRoute);
                        }
                        else {
                            RoutesBetweenNodesPairsCollection.Add(new RoutesBetweenNodesPair(0, endNodeNumber,
                                new List<Route>()));

                            routesForNodes =
                                RoutesBetweenNodesPairsCollection.FirstOrDefault(
                                    x => x.StartNodeNumber == 0 && x.EndNodeNumber == endNodeNumber);

                            routesForNodes?.RoutesCollection.Add(currentRoute);
                        }
                    }
                    counter++;

                    if (counter == 31) {
                        counter = 1;
                        endNodeNumber++;
                    }
                }
                file.Close();
                return true;
            }
            catch (Exception ex) {
                return false;
                throw;
            }
        }

        /// <summary>
        /// Load the slots from file. Must be executed after LoadRoutes method
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadSlots(string path) {

            int counter = 0;
            int endNodeNumber = 0;
            string line;
            try {
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null) {
                  
                        List<int> currentSlotList = line.Split('\t').Select(Int32.Parse).ToList();
                    
                        var routesForNodes =
                            RoutesBetweenNodesPairsCollection.FirstOrDefault(
                                x => x.StartNodeNumber == 0 && x.EndNodeNumber == endNodeNumber);

                        if (routesForNodes != null) {
                            routesForNodes.RoutesCollection.ElementAt(counter).SlotsList = currentSlotList;
                        }
                        else {
                            throw new Exception("You have to Load routes first!");
                        }
                   
                    
                    counter++;

                    if (counter == 30) {
                        counter = 0;
                        endNodeNumber++;
                    }
                }
                file.Close();
                return true;
            }
            catch (Exception ex) {
                return false;
                
            }
            
        }

   

        private Route CalculateRouteFromBinary(List<int> coll) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in coll)
            {
                if (element == 1)
                    result.Add(counter);
                counter++;
            }

            return new Route(result);
        }

        public List<RoutesBetweenNodesPair> GetRoutesBetweenNodes(int startNode, int endNode){
            List<RoutesBetweenNodesPair> coll = new List<RoutesBetweenNodesPair>();
            if (RoutesBetweenNodesPairsCollection != null && RoutesBetweenNodesPairsCollection.Any()) {
                coll =
                    RoutesBetweenNodesPairsCollection.Where(
                        x => x.StartNodeNumber == startNode && x.EndNodeNumber == endNode).ToList();
            }
            return coll;
        }
    }
}