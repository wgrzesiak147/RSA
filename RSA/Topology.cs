using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class Topology
    {
        public int Edges;
        public int Size;
        public int[][] CurrentTopology;

        public bool LoadTopology(string path){
            int counter = 0;
            string line;
            try
            {
                // Read the file and display it line by line.
                StreamReader file = new StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    if (counter == 0)
                    {
                        int size = 0;
                        Int32.TryParse(line, out size);

                        if (size == 0)
                            throw new Exception("Size must be higher than 0!");

                        InitializeTopology(size);
                    }
                    else if (counter == 1)
                    {
                        Int32.TryParse(line, out Edges);
                    }
                    else
                    {
                        CurrentTopology[counter - 2] = line.Split('\t').Select(Int32.Parse).ToArray();
                    }
                    counter++;
                }
                file.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        private void InitializeTopology(int size){
            CurrentTopology = new int[size][];

            for (int i = 0; i < size; i++)
            {
                CurrentTopology[i] = new int[size];
            }
        }
    }
}