using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RSA.Entities;
using RSA.Helpers;

namespace RSA {
    public class RoutesManager {
        public static int RoutesCount { get; set; } = 0;
        public int RoutesQuantity;
        public List<Route> allRoutes = new List<Route>();

        /// <summary>
        /// Loading routes from file and calculating parents and childs 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadRoutes(string path) {
            try {
                #region Route loading snippet
                // Read the file and display it line by line.
                using (StreamReader file = new StreamReader(path)) {
                    int lineCounter = 0;
                    int startNodeNumber = 0;
                    int endNodeNumber = 1;
                    string line;
                    while ((line = file.ReadLine()) != null) {
                        if (lineCounter == 0) { Int32.TryParse(line, out RoutesQuantity); lineCounter++; }
                        else {
                            List<int> _line = line.Split(' ').Select(Int32.Parse).ToList();
                            if (startNodeNumber == endNodeNumber) { endNodeNumber++; }
                            Route currRoute = CalculateRouteFromBinary(_line, startNodeNumber, endNodeNumber);
                            //Incrementing index for route
                            if (currRoute != null) {
                                RoutesCount++;
                                allRoutes.Add(currRoute);
                            }
                            #region Uncoment for simple checkup of line reading ;)
                            //if (lineCounter%390 == 0) {
                            //    int i = 0;
                            //    i = 1 + 2;
                            //} 
                            #endregion
                            if (lineCounter % 30 == 0) { endNodeNumber++; }
                            if (lineCounter % 390 == 0) { startNodeNumber++; endNodeNumber = 0; }
                            lineCounter++;
                        }
                    } // All routes are loaded - We need now to add these routes to a structure we want to 
                }
                #endregion

                #region Uncomment to see that every start node has the same number of routes - 390; Or we can just implement proper routing table read here :) - TODO
                //foreach (var routeGroup in allRoutes.GroupBy(_startNodeIndex => _startNodeIndex.StartNode)
                //                                     .Select(group => new {
                //                                         StartNode = group.Key,
                //                                         Count = group.Count()
                //                                     }).OrderBy(x => x.StartNode)) {
                //    Console.WriteLine("StartNode {0} has {1} routes", routeGroup.StartNode, routeGroup.Count);
                //} 
                #endregion
                //This is MUCH faster than loading with method used before...
                #region Loading routes to NodeRoutingTable structures to be later used from its static RoutingTable list
                if (allRoutes.Count > 0) {
                        for (int startNode = 0; startNode < 14; startNode++) {
                            for (int endNode = 0; endNode < 14; endNode++) {
                                if (startNode == endNode) { endNode++; }
                                // Now we can use Linq to find routes for proper NodeRoutingTable feed
                                List<Route> nodeRoutes = allRoutes.FindAll(x => x.StartNode == startNode && x.EndNode == endNode);
                                if (nodeRoutes.Count > 0) {
                                    NodeRoutingTable newNodeRoutingTable = new NodeRoutingTable(startNode, endNode, nodeRoutes);
                                    newNodeRoutingTable.PrintObjToConsole();
                                }
                            }
                        }
                    } // Phew - that was painful, sorry... 
                    #endregion
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        /// <summary>
        /// Initializing childs and parents for any existing routes in routesCollection. It can take a while!
        /// </summary>
        /// <param name="currentRoute"></param>
        // This method (or something similiar) should be called after loading ALL routes!!! - TOFIX
        private void InitializeChildOrParents(Route currentRoute){
            foreach (var routesBetweenNodes in NodeRoutingTable.RoutingTable) // for each nodePair
            {
                if (routesBetweenNodes == null ||
                    routesBetweenNodes.RoutesCollection == null) //if the pair exists and have some routes
                { return; }

                List<Route> routeList = routesBetweenNodes.RoutesCollection;
                foreach (var element in routeList) //for each route in each nodePair
                {
                    if (element.NodeList.ContainsSubsequence(currentRoute.NodeList)) //checking if the route is a parent or child
                    {
                        currentRoute.ParentsRoutes.Add(element);
                        element.ChildsRoutes.Add(currentRoute);
                    } else if (currentRoute.NodeList.ContainsSubsequence(element.NodeList)) {
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
        // Funkcja powinna zostać przepisana na bardziej zrozumiałą - nie trzeba wykorzystywać funkcji GetRoutesBetweenNodes - wykorzystanie tylu FirstOrDefault jest bolesne... - TOFIX - TODO
        public bool LoadSlots(string path) {
            int counter = 0;
            int startNodeNumber = 0;
            int endNodeNumber = 1;
            string line;
            try {
                // Read the file and display it line by line.
                using (StreamReader file = new StreamReader(path)) {
                    while ((line = file.ReadLine()) != null) {
                        int[] currentSlotList = line.Split('\t').Select(Int32.Parse).ToArray();
                            // spliting line by '\t' and parsing every element to int. Then converting it to array
                        var routesForNodes = GetRoutesBetweenNodes(startNodeNumber, endNodeNumber);
                            //getting RoutesBetweenNodePair entity for current startNode and endNode
                        if (routesForNodes != null) {
                            //checking if  RoutesBetweenNodePair entity exists
                            routesForNodes.RoutesCollection.ElementAt(counter).SlotsList = currentSlotList;
                            //if yes adding slots to RoutesBetweenNodePair current route
                        } else {
                            throw new Exception("You have to Load routes first!");
                        }
                        counter++;
                        if (counter == 30) {
                            counter = 0;
                            endNodeNumber++;
                            if (endNodeNumber == 31) {
                                startNodeNumber++;
                                endNodeNumber = 0;
                            }
                            if (startNodeNumber == endNodeNumber) {
                                endNodeNumber++;
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex) {
                return false;  
            }
        }

   
        /// <summary>
        /// Converting binary list to Route
        /// </summary>
        /// <param name="coll"></param>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        private Route CalculateRouteFromBinary(List<int> coll,int startNode,int endNode) {
            List<int> result = new List<int>();
            int counter = 0;
            foreach (var element in coll) {
                if (element == 1) //if element == 1 it means that this link is used in this route
                    result.Add(counter);
                counter++;
            }
            return new Route(RoutesCount, result, startNode,endNode);
        }

        /// <summary>
        /// Getting the RoutesBeweenNodePair entity with specified startNode and endNode from RoutesBetweenNodePairsCollection
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public NodeRoutingTable GetRoutesBetweenNodes(int startNode, int endNode){
            if (NodeRoutingTable.RoutingTable != null && NodeRoutingTable.RoutingTable.Any())
            {
                NodeRoutingTable result = NodeRoutingTable.RoutingTable.FirstOrDefault(
                    x => x.StartNodeNumber == startNode && x.EndNodeNumber == endNode);
                return result;
            }
            return null;
        }

     
    }
}