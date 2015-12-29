using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
   public class RequestManager
   {

       public List<Request> CurrentRequestList = new List<Request>();
       public int RequestQuantity;


        /// <summary>
        /// Loading requests from file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
       public bool LoadRequests(string path) {
            int counter = 0;
            string line;
            try
            {
                // Read the file and display it line by line.
                using (StreamReader file = new StreamReader(path)) {
                    while ((line = file.ReadLine()) != null) {
                        if (counter ==0) {
                            Int32.TryParse(line, out RequestQuantity);
                        }
                        else {
                            Request req = GetRequestFromRow(line);
                            CurrentRequestList.Add(req);
                        }
                       
                        counter++;
                    }
                }
               
                return true;
            }
            catch (Exception ex) {
                return false;
               
            }
            
        }

       private Request GetRequestFromRow(string line){

           int startNode =  Int32.Parse(line.Substring(0, 3).Trim());
           int endNode = Int32.Parse(line.Substring(2, 3).Trim());
           int capacity = Int32.Parse(line.Substring(5).Trim()); 
           Request result = new Request(startNode,endNode,capacity);
           return result;
       }
   }
}
