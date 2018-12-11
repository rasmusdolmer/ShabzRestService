using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Log
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
        public int LockId { get; set; }
        private static int NextId = 1;

        public Log()
        {
            
        }

        public Log(int accountId, string date, bool status, int lockId)
        {
            Id = NextId++;
            AccountId = accountId;
            Date = date;
            Status = status;
            LockId = lockId;
        }

        public Log(int id, int accountId, string date, bool status, int lockId)
        {
            Id = id;
            AccountId = accountId;
            Date = date;
            Status = status;
            LockId = lockId;
        }
    }
}
