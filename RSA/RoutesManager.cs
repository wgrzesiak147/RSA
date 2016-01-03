using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RSA.Entities;
using RSA.Helpers;

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

                        Route currentRoute = CalculateRouteFromBinary(coll,startNodeNumber,endNodeNumber);

                        var routesForNodes = GetRoutesBetweenNodes(startNodeNumber, endNodeNumber);

                        if (routesForNodes == null) {
                            RoutesBetweenNodesPairsCollection.Add(new RoutesBetweenNodesPair(startNodeNumber, endNodeNumber,
                                new List<Route>()));

                            routesForNodes = GetRoutesBetweenNodes(startNodeNumber, endNodeNumber);

                        }
                        InitializeChildOrParents(currentRoute);
                        routesForNodes?.RoutesCollection.Add(currentRoute);
                    }
                    counter++;

                    //TODO : TESTS!
                    if (counter == 31) { //every 30 lines (1 because the first row is size)
                        counter = 1;  //restarting counter
                        endNodeNumber++; //increasing endNode
                        if (endNodeNumber == 31){ //When endNode will be equal to 30
                            startNodeNumber++; //increasing startNode
                            endNodeNumber = 0; //restarting endNode
                        }
                        if (startNodeNumber == endNodeNumber) //When startNode equal to endNode
                            endNodeNumber++; //increasing endNode
                    }
                }
                file.Close();
                return true;
            }
            catch (Exception ex) {
                return false;
                
            }
        }

        private void InitializeChildOrParents(Route currentRoute)
        {
            //RoutesBetweenNodesPair routesBetweenNodes = GetRoutesBetweenNodes(currentRoute.StartNode,
            //    currentRoute.EndNode);

            //if(routesBetweenNodes == null || routesBetweenNodes.RoutesCollection == null)
            //    return;

            foreach (var routesBetweenNodes in RoutesBetweenNodesPairsCollection) // for each nodePair
            {
                if (routesBetweenNodes == null || routesBetweenNodes.RoutesCollection == null) //if the pair exists and have some routes
                    return;

                List<Route> routeList = routesBetweenNodes.RoutesCollection;

                foreach (var element in routeList) //for each route in each nodePair
                {
                    if (element.NodeList.ContainsSubsequence(currentRoute.NodeList)) //checking if the route is a parent or child
                    {
                        currentRoute.ParentsRoutes.Add(element);
                        element.ChildsRoutes.Add(currentRoute);
                    }
                    else if (currentRoute.NodeList.ContainsSubsequence(element.NodeList))
                    {
                        currentRoute.ChildsRoutes.Add(element);
                        element.ParentsRoutes.Add(currentRoute);
                    }
                }

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

                        var routesForNodes = GetRoutesBetweenNodes(startNodeNumber, endNodeNumber); //getting RoutesBetweenNodePair entity for current startNode and endNode

                        if (routesForNodes != null) {   //checking if  RoutesBetweenNodePair entity exists
                            routesForNodes.RoutesCollection.ElementAt(counter).SlotsList = currentSlotList; //if yes adding slots to RoutesBetweenNodePair current route
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

   

        private Route CalculateRouteFromBinary(List<int> coll,int startNode,int endNode) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in coll)
            {
                if (element == 1)
                    result.Add(counter);
                counter++;
            }

            return new Route(result,startNode,endNode);
        }

        /// <summary>
        /// Getting the RoutesBeweenNodePair entity with specified startNode and endNode from RoutesBetweenNodePairsCollection
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public RoutesBetweenNodesPair GetRoutesBetweenNodes(int startNode, int endNode){

            if (RoutesBetweenNodesPairsCollection != null && RoutesBetweenNodesPairsCollection.Any())
            {
                RoutesBetweenNodesPair result = RoutesBetweenNodesPairsCollection.FirstOrDefault(
                    x => x.StartNodeNumber == startNode && x.EndNodeNumber == endNode);
                return result;
            }

            return null;
        }

     
    }
}