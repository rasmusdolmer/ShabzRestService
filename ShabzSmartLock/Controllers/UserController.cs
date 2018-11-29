﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShabzSmartLock.Model;

namespace ShabzSmartLock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private static List<Account> AccountList = new List<Account>()
        {
            new Account("Frederik"),
            new Account("Kian"),
            new Account("Lucas"),
            new Account("Marcus"),
            new Account("Rasmus")
        };

        // GET: api/Account
        [HttpGet]
        public List<Account> Get()
        {
            return AccountList;
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "GetAccount")]
        public Account GetAccount(int id)
        {
            var Account = AccountList.FirstOrDefault(l => l.Id == id);

            if (Account != null)
            {
                return Account;
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }

        // GET: api/Account/5
        [HttpGet("{id}/locks", Name = "GetAccountsLocks")]
        public Lock GetAccountsLocks(int id)
        {
            var AccountLock = LockController.LockList.FirstOrDefault(l => l.AccountId == id);

            if (AccountLock != null)
            {
                return AccountLock;
            }
            else
            {
                throw new Exception("Denne bruger har ingen tilknyttede locks");
            }
        }

        // GET: api/Account/5
        [HttpGet("{id}/logs", Name = "GetAccountsLogs")]
        public List<Log> GetAccountsLogs(int id)
        {
            var AccountLogs = (from log in LogController.LogList where log.AccountId == id select log).ToList();

            if (AccountLogs.Count > 0)
            {
                return AccountLogs;
            }
            else
            {
                throw new Exception("Denne bruger har ingen tilknyttede logs");
            }
        }

        // POST: api/Account
        [HttpPost]
        public void Post(Account u)
        {
            AccountList.Add(new Account(u.Name));
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, string name)
        {
            var AccountToUpdate = AccountList.FirstOrDefault(u => u.Id == id);
            if (AccountToUpdate != null)
            {
                AccountToUpdate.Name = name;
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var AccountToDelete = AccountList.FirstOrDefault(u => u.Id == id);
            if (AccountToDelete != null)
            {
                AccountList.Remove(AccountToDelete);
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }
    }
}
