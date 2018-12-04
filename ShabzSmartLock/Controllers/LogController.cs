using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private const string Conn =
            "Data Source=gruppe-3.database.windows.net;Initial Catalog=lager;User ID=goldmann;Password=Doodlejump123";
        public static List<Log> LogList = new List<Log>();

        // GET: api/Log
        [HttpGet]
        public List<Log> Get()
        {
            LogList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_log", dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LogList.Add(new Log(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(), Convert.ToBoolean(reader[3])));
                        }
                    }
                }
            }
            return LogList;
        }

        // GET: api/Log/5
        [HttpGet("{id}", Name = "GetLog")]
        public Log GetSingleLog(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_log WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Log log = new Log(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(), Convert.ToBoolean(reader[3]));

                            return log;
                        }
                    }
                }
            }

            return null;
        }

        // POST: api/Log
        [HttpPost]
        public void Post(Log log)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO shabz_log (AccountId, Date, Status) VALUES (@accountId, @date, @status)", dbConnection))
                {
                    command.Parameters.AddWithValue("@accountId", log.AccountId);
                    command.Parameters.AddWithValue("@date", log.Date);
                    command.Parameters.AddWithValue("@status", log.Status);
                    command.ExecuteNonQuery();
                    LogList.Add(new Log(log.AccountId, log.Date, log.Status));
                }
            }
        }

        // PUT: api/Log/5
        [HttpPut("{id}")]
        public void Put(int id,[FromBody]Log log)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE shabz_log SET AccountId = @accountId, Date = @date, Status = @status WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.Add("@accountId", SqlDbType.Int);
                    command.Parameters.Add("@date", SqlDbType.Date);
                    command.Parameters.Add("@status", SqlDbType.Bit);
                    command.ExecuteNonQuery();
                    var logToUpdate = LogList.FirstOrDefault(l => l.Id == id);
                    logToUpdate.AccountId = log.AccountId;
                    logToUpdate.Status = log.Status;
                    logToUpdate.Date = log.Date;
                }
            }
        }

        // DELETE: api/Log
        [HttpDelete]
        public void Delete(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("DELETE from shabz_log WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();

                    var logToDelete = LogList.FirstOrDefault(l => l.Id == id);
                    LogList.Remove(logToDelete);

                }
            }
        }
    }
}
