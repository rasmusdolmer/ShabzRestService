using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Log
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        private static int NextId = 1;

        public Log()
        {
            
        }

        public Log(int userId, string date, string status)
        {
            Id = NextId++;
            UserId = userId;
            Date = date;
            Status = status;
        }
    }
}
