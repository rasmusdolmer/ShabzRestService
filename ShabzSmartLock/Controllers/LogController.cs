using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShabzSmartLock.Model;

namespace ShabzSmartLock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        public static List<Log> LogList = new List<Log>()
        {
            new Log(2, "29-11-2018 10:00", "Locked")
        };

        // GET: api/Log
        [HttpGet]
        public List<Log> Get()
        {
            return LogList;
        }

        // GET: api/Log/5
        [HttpGet("{id}", Name = "GetLog")]
        public Log GetSingleLog(int id)
        {
            var singleLog = LogList.FirstOrDefault(l => l.Id == id);

            return singleLog;
        }

        // POST: api/Log
        [HttpPost]
        public void Post([FromBody] Log log)
        {
            LogList.Add(new Log(log.UserId, log.Date, log.Status));
        }

        // PUT: api/Log/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Log log)
        {
            var logToUpdate = LogList.FirstOrDefault(l => l.Id == id);
            if (logToUpdate != null)
            {
                logToUpdate.UserId = log.UserId;
                logToUpdate.Status = log.Status;
                logToUpdate.Date = log.Date;
            }
        }

        // DELETE: api/Log
        [HttpDelete]
        public void Delete()
        {
            LogList.Clear();
        }
    }
}
