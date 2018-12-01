using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ShabzSmartLock.Model;

namespace ShabzSmartLock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private static List<Account> AccountList = new List<Account>()
        {
            new Account("Frederik","f@f.f", 1),
            new Account("Kian","k@k.k", 2),
            new Account("Lucas","l@l.l", 3),
            new Account("Marcus","m@m.m", 4),
            new Account("Rasmus","r@r.r", 5)
        };

        // GET: api/Account
        [HttpGet]
        public List<Account> Get()
        {
            return AccountList;
        }

        // GET: api/Account/1
        [HttpGet]
        [Route("{input}")]
        public Account GetAccount(string input)
        {
            if (input.Contains("@"))
            {
                var AccountEmail = AccountList.FirstOrDefault(a => a.Email == input);

                if (AccountEmail != null)
                {
                    return AccountEmail;
                }

                return null;
            }
            var AccountId = AccountList.FirstOrDefault(a => a.Id.ToString() == input);

            if (AccountId != null)
            {
                return AccountId;
            }

            return null;
        }

        // GET: api/Account/5
        [HttpGet("{id}/locks", Name = "GetAccountsLocks")]
        public Lock GetAccountsLocks(int id)
        {
            //var AccountLock = LockController.LockList.FirstOrDefault(l => l.AccountId == id);

            //if (AccountLock != null)
            //{
            //    return AccountLock;
            //}
            //else
            //{
            //    throw new Exception("Denne bruger har ingen tilknyttede locks");
            //}
            return null;
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
            return null;
        }

        // POST: api/Account
        [HttpPost]
        public void Post(Account u)
        {
            AccountList.Add(new Account(u.Name, u.Email, u.PrimaryLock));
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public string Put(int id, string name, string email, int primaryLock)
        {
            var AccountToUpdate = AccountList.FirstOrDefault(u => u.Id == id);
            if (AccountToUpdate != null)
            {
                AccountToUpdate.Name = name;
                AccountToUpdate.Email = email;
                AccountToUpdate.PrimaryLock = primaryLock;
                return "Brugeren med id: " + id + " blev opdateret";
            }

            return "Der findes ikke en bruger med dette id";

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            var AccountToDelete = AccountList.FirstOrDefault(u => u.Id == id);
            if (AccountToDelete != null)
            {
                AccountList.Remove(AccountToDelete);
                return "Brugeren med id: " + id + " blev slettet";
            }

            return "Der findes ikke en bruger med dette id";
        }
    }
}
