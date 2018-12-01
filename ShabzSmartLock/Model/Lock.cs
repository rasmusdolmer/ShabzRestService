using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Lock
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public bool Status { get; set; }
        public DateTime DateRegistered { get; set; }
        public List<Log> LogList { get; set; }
        private static int NextId = 1;

        public Lock()
        {
            
        }

        public Lock(string name, string accessCode, bool status, DateTime dateRegistered, List<Log> logList)
        {
            Id = NextId++;
            Name = name;
            AccessCode = accessCode;
            Status = status;
            DateRegistered = dateRegistered;
            LogList = logList;
        }
    }
}
