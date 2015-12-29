using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class TopologyManager {

        public Topology CurrentTopology = new Topology();


        /// <summary>
        /// Load Topology from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool LoadTopology(string path){
            int counter = 0;
            string line;
            try {
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null) {
                    if (counter == 0) {
                        int size;
                        Int32.TryParse(line, out size);  //parsing first line as a size of the topology

                        if (size == 0)
                            throw new Exception("Size must be higher than 0!");

                        InitializeTopology(size);  // Initializing Topology(array) with size
                    }
                    else if (counter == 1){
                        Int32.TryParse(line, out CurrentTopology.Edges);   //Parsing second line as edges value storend in CurentTopology object
                    }
                    else {
                        CurrentTopology.CurrentTopology[counter - 2] = line.Split('\t').Select(Int32.Parse).ToArray();
                    }
                    counter++;
                }
                file.Close();
                return true;
            }
            catch (Exception ex) {
                return false; // If there will be any error catched the method will return false. It will allow to check if its properly loaded or no
               
            }
        }
        /// <summary>
        /// Initializing array with topology
        /// </summary>
        /// <param name="size"></param>
        private void InitializeTopology(int size){
            CurrentTopology.CurrentTopology = new int[size][];

            for (int i = 0; i < size; i++) {
                CurrentTopology.CurrentTopology[i] = new int[size];
            }
        }

    }
}
