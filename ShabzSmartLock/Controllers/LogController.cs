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
        public List<Log> Get(int count)
        {
            LogList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();
                count = 1;
                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_log ORDER BY Date", dbConnection))
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
        [HttpGet("{count}", Name = "GetLog")]
        public List<Log> GetSingleLog(int count)
        {
            LogList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_log ORDER BY Date DESC OFFSET 15 * '" + count + "' ROWS FETCH NEXT 15 ROWS ONLY", dbConnection))
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

        //// GET: api/Log
        //[HttpGet("/{count}")]
        //public List<Log> GetLimitedLogs(int count)
        //{
        //    LogList.Clear();
        //    using (SqlConnection dbConnection = new SqlConnection(Conn))
        //    {
        //        dbConnection.Open();

        //        using (SqlCommand command = new SqlCommand("SELECT TOP 15 * FROM shabz_log ORDER BY Date", dbConnection))
        //        {
        //            command.Parameters.AddWithValue("@count", count);
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    LogList.Add(new Log(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(), Convert.ToBoolean(reader[3])));
        //                }
        //            }
        //        }
        //    }
        //    return LogList;
        //}

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
                    command.Parameters.AddWithValue("@date", DateTime.Parse(log.Date));
                    command.Parameters.AddWithValue("@status", log.Status);
                    command.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Log/5
        [HttpPut("{id}")]
        public void Put(int id, Log log)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE shabz_log SET AccountId = @accountId, Date = @date, Status = @status WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@accountId", log.AccountId);
                    command.Parameters.AddWithValue("@date", DateTime.Parse(log.Date));
                    command.Parameters.AddWithValue("@status", log.Status);
                    command.ExecuteNonQuery();
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
                }
            }
        }

        // DELETE: api/Log/DeleteAll
        [HttpDelete("/deleteall")]
        public void DeleteAll()
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("DELETE from shabz_log", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
