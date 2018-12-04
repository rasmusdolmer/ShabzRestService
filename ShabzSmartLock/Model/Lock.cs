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
        public string DateRegistered { get; set; }
        private static int NextId = 1;

        public Lock()
        {
            
        }

        public Lock(string name, string accessCode, bool status)
        {
            Id = NextId;
            Name = name;
            AccessCode = accessCode;
            Status = status;
        }

        public Lock(int id, string name, string accessCode, bool status)
        {
            Id = id;
            Name = name;
            AccessCode = accessCode;
            Status = status;
        }

        public Lock(int id, string name, string accessCode, bool status, string dateRegistered)
        {
            Id = id;
            Name = name;
            AccessCode = accessCode;
            Status = status;
            DateRegistered = dateRegistered;
        }
    }
}
