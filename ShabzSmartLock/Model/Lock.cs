using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Lock
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public List<Log> LogList { get; set; }
        private static int NextId = 1;

        public Lock()
        {
            
        }

        public Lock(int userId, bool status, List<Log> logList)
        {
            Id = NextId++;
            UserId = userId;
            Status = status;
            LogList = logList;
        }
    }
}
