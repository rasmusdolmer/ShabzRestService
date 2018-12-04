using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Account
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public int PrimaryLockId { get; set; }
        private static int NextId = 1;

        public Account()
        {
            
        }

        public Account(string email, string name, int primaryLockId)
        {
            Id = NextId++;
            Email = email;
            Name = name;
            PrimaryLockId = primaryLockId;
        }
        public Account(int id, string email, string name, int primaryLockId)
        {
            Id = id;
            Email = email;
            Name = name;
            PrimaryLockId = primaryLockId;
        }
    }
}
