using System;
using System.Collections.Generic;

namespace Puppy.Model.Message
{
    public class EN_US : IMessage
    {
        private readonly Dictionary<int, string> list = 
         new Dictionary<int, string>()
            {
                {210,"Insert {0} is successfully."},
                {220,"Get data is successfully."},
                {410, "Insert {0} isn't successfully."},
                {420, "Data is not found."},
                {500, "Internal Error."}
            }; 

        public Dictionary<int, string> MessageList
        {   get { return list; } }

        public string GetMessage(int key)
        {
            return MessageList[key];
        }
              
    }
}