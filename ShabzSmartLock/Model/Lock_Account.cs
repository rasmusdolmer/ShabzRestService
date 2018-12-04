using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Lock_Account
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int LockId { get; set; }
        public int RoleId { get; set; }
        private static int NextId = 1;

        public Lock_Account()
        {

        }

        public Lock_Account(int accountId, int lockId, int roleId)
        {
            Id = NextId++;
            AccountId = accountId;
            LockId = lockId;
            RoleId = roleId;
        }

        public Lock_Account(int id, int accountId, int lockId, int roleId)
        {
            Id = id;
            AccountId = accountId;
            LockId = lockId;
            RoleId = roleId;
        }
    }

}
