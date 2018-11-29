using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PrimaryLock { get; set; }
        private static int NextId = 1;

        public Account()
        {
            
        }

        public Account(string name, string email, int primaryLock)
        {
            Id = NextId;
            Name = name;
            Email = email;
            PrimaryLock = primaryLock;
        }
    }
}
