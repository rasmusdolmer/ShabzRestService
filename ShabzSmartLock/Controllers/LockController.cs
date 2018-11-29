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
            new Lock("Hoveddør", "123abc", true, DateTime.Parse("16/07/98 15:00:00"),new List<Log>()
            {
                new Log(1,DateTime.Now, false),
                new Log(2,DateTime.Now, true),
                new Log(3,DateTime.Now, false)
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
        public void Post(Lock l)
        {
            LockList.Add(new Lock(l.Name, l.AccessCode, false, l.DateRegistered, new List<Log>()));
        }

        // PUT: api/Lock/5
        [HttpPut("{id}")]
        public void Put(int id, string name, string accessCode, bool status, DateTime dateRegistered)
        {
            var lockToUpdate = LockList.FirstOrDefault(l => l.Id == id);
            if (lockToUpdate != null)
            {
                lockToUpdate.Name = name;
                lockToUpdate.AccessCode = accessCode;
                lockToUpdate.Status = !lockToUpdate.Status;
                lockToUpdate.DateRegistered = dateRegistered;
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
