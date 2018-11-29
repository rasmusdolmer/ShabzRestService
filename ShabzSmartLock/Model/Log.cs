﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Log
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        private static int NextId = 1;

        public Log()
        {
            
        }

        public Log(int accountId, DateTime date, bool status)
        {
            Id = NextId;
            AccountId = accountId;
            Date = date;
            Status = status;
        }}
    }
}
