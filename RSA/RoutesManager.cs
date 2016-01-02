using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RSA.Entities;

namespace RSA {
    public class RoutesManager {
        public int RoutesQuantity;
        public List<RoutesBetweenNodesPair> RoutesBetweenNodesPairsCollection = new List<RoutesBetweenNodesPair>();
       
        /// <summary>
        /// Loading routes from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadRoutes(string path) {
          
            int counter = 0;
            int startNodeNumber = 0;
            int endNodeNumber = 1;
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
                                x => x.StartNodeNumber == startNodeNumber && x.EndNodeNumber == endNodeNumber);

                        if (routesForNodes == null) {
                            RoutesBetweenNodesPairsCollection.Add(new RoutesBetweenNodesPair(startNodeNumber, endNodeNumber,
                                new List<Route>()));

                            routesForNodes =
                                RoutesBetweenNodesPairsCollection.FirstOrDefault(
                                    x => x.StartNodeNumber == startNodeNumber && x.EndNodeNumber == endNodeNumber);

                           }
                        routesForNodes?.RoutesCollection.Add(currentRoute);
                    }
                    counter++;

                    //TODO : TESTS!
                    if (counter == 31) { //every 30 lines (1 because the first row is size)
                        counter = 1;  //restarting counter
                        endNodeNumber++; //increasing endNodeNumber
                        if (endNodeNumber == 31){ //When endNodeNumber will be equal to 30
                            startNodeNumber++; //increasing startNodeNumber
                            endNodeNumber = 0; //restarting endNodeNumber
                        }
                        if (startNodeNumber == endNodeNumber) //When startNodeNumber equal to endNodeNumber
                            endNodeNumber++; //increasing endNodeNumber
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
            int startNodeNumber = 0;
            int endNodeNumber = 1;
            string line;
            try {
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null) {
                  
                        int[] currentSlotList = line.Split('\t').Select(Int32.Parse).ToArray(); // spliting line by '\t' and parsing every element to int. Then converting it to array
                    
                        var routesForNodes =
                            RoutesBetweenNodesPairsCollection.FirstOrDefault(
                                x => x.StartNodeNumber ==startNodeNumber && x.EndNodeNumber == endNodeNumber);

                        if (routesForNodes != null) {
                            routesForNodes.RoutesCollection.ElementAt(counter).SlotsList = currentSlotList;
                        }
                        else {
                            throw new Exception("You have to Load routes first!");
                        }
                   
                    
                    counter++;

                    if (counter == 30){ 
                        counter = 0;  
                        endNodeNumber++; 
                        if (endNodeNumber == 31)
                        { 
                            startNodeNumber++; 
                            endNodeNumber = 0; 
                        }
                        if (startNodeNumber == endNodeNumber) 
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

        /// <summary>
        /// Load weights for current List<RoutesBetweenNodesPair>
        /// </summary>
        /// <param name="currentTopology"></param>
        public void LoadWeights(Topology currentTopology)
        {

            int size = currentTopology.CurrentTopology.Length;

            for (int i = 0; i < size - 1; i++)
            {
              //  RoutesBetweenNodesPairsCollection.FirstOrDefault(x => x.EndNodeNumber == i);

            }
        }
    }
}