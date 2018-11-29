using System;
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
    public class LockController : ControllerBase
    {
        public static List<Lock> LockList = new List<Lock>()
        {
            new Lock(5, true, new List<Log>()
            {
                new Log(1,"e","j"),
                new Log(2,"e","d"),
                new Log(3,"i","g")
            })
        };

        // GET: api/Lock
        [HttpGet]
        public List<Lock> Get()
        {
            return LockList;
        }

        // GET: api/Lock/5
        [HttpGet("{id}", Name = "GetLock")]      
        public Lock Get(int id)
        {
            var singleLock = LockList.FirstOrDefault(l => l.Id == id);

            if (singleLock != null)
            {
                return singleLock;
            }
            else
            {
                throw new Exception("Der findes ikke en Lås med dette id.");
            }
        }

        // GET: api/Lock/5
        [HttpGet("{id}/log", Name = "GetLockLogList")]
        public List<Log> GetLockLogList(int id)
        {
            var singleLock = LockList.FirstOrDefault(l => l.Id == id);

            if (singleLock != null)
            {
                return singleLock.LogList;
            }
            else
            {
                throw new Exception("Der findes ikke en Lås med dette id.");
            }
        }

        // GET: api/Lock/5
        [HttpGet("{lockId}/log/{logId}", Name = "GetLockSingleLog")]
        public Log GetLockSingleLog(int lockId, int logId)
        {
            var singleLock = LockList.FirstOrDefault(l => l.Id == lockId);

            if (singleLock != null)
            {
                var singleLog = singleLock.LogList.FirstOrDefault(l => l.Id == logId);
                return singleLog;
            }
            else
            {
                throw new Exception("Der findes ikke en Lås med dette id.");
            }
        }

        // POST: api/Lock
        [HttpPost]
        public void Post(int AccountId)
        {
            LockList.Add(new Lock(AccountId, false, new List<Log>()));
        }

        // PUT: api/Lock/5
        [HttpPut("{id}")]
        public void Put(int id, int AccountId)
        {
            var lockToUpdate = LockList.FirstOrDefault(l => l.Id == id);
            if (lockToUpdate != null)
            {
                lockToUpdate.AccountId = AccountId;
                lockToUpdate.Status = !lockToUpdate.Status;
                lockToUpdate.LogList = lockToUpdate.LogList;
            }
            else
            {
                throw new Exception("Der findes ikke en Lås med dette id.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lockToDelete = LockList.FirstOrDefault(l => l.Id == id);
            if (lockToDelete != null)
            {
                LockList.Remove(lockToDelete);
            }
            else
            {
                throw new Exception("Der findes ikke en Lås med dette id.");
            }
        }
    }
}
