using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
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
        private const string Conn =
            "Data Source=gruppe-3.database.windows.net;Initial Catalog=lager;User ID=goldmann;Password=Doodlejump123";
        public static List<Lock> LockList = new List<Lock>();

        // GET: api/Lock
        [HttpGet]
        public List<Lock> Get()
        {
            LockList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_lock", dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lock lås;
                            if (reader[4].ToString().Length > 0)
                            {
                                lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToBoolean(reader[3]), reader[4].ToString());
                            }
                            else
                            {
                                lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToBoolean(reader[3]));
                            }

                            LockList.Add(lås);
                        }
                    }
                }
            }

            return LockList;
        }

        // GET: api/Lock/5
        [HttpGet("{input}", Name = "GetLock")]
        public Lock Get(string input)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                if (int.TryParse(input, out int lockId))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_lock WHERE Id = @id", dbConnection))
                    {
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(input));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lock lås;
                                if (reader[4].ToString().Length > 0)
                                {
                                    lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(),
                                        reader[2].ToString(), Convert.ToBoolean(reader[3]),
                                        reader[4].ToString());
                                }
                                else
                                {
                                    lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(),
                                        reader[2].ToString(), Convert.ToBoolean(reader[3]));
                                }

                                return lås;
                            }
                        }
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_lock WHERE AccessCode = @accessCode", dbConnection))
                    {
                        command.Parameters.AddWithValue("@accessCode", input);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lock lås;
                                if (reader[4].ToString().Length > 0)
                                {
                                    lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(),
                                        reader[2].ToString(), Convert.ToBoolean(reader[3]),
                                        reader[4].ToString());
                                }
                                else
                                {
                                    lås = new Lock(Convert.ToInt32(reader[0]), reader[1].ToString(),
                                        reader[2].ToString(), Convert.ToBoolean(reader[3]));
                                }

                                return lås;
                            }
                        }
                    }
                }

                return null;
            }
        }
        //// GET: api/Lock/5
        //[HttpGet("{id}/log", Name = "GetLockLogList")]
        //public List<Log> GetLockLogList(int id)
        //{
        //    var singleLock = LockList.FirstOrDefault(l => l.Id == id);

        //    if (singleLock != null)
        //    {
        //        return singleLock.LogList;
        //    }

        //    return null;
        //}

        // POST: api/Lock
        [HttpPost]
        public void Post(Lock l)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("INSERT INTO shabz_lock (Name, AccessCode, Status) VALUES (@name, @accessCode, @status)", dbConnection))
                {
                    command.Parameters.AddWithValue("@name", l.Name);
                    command.Parameters.AddWithValue("@accessCode", l.AccessCode);
                    command.Parameters.AddWithValue("@status", l.Status);

                    command.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Lock/5
        [HttpPut("{id}")]
        public void Put(int id, Lock lck)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE shabz_lock SET name = @name, accessCode = @accessCode, status = @status, DateRegistered = @dateRegistered WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", lck.Name);
                    command.Parameters.AddWithValue("@accessCode", lck.AccessCode);
                    command.Parameters.AddWithValue("@status", lck.Status);
                    command.Parameters.AddWithValue("@dateRegistered", DateTime.Parse(lck.DateRegistered));

                    command.ExecuteNonQuery();
                }
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("DELETE from shabz_lock WHERE Id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
