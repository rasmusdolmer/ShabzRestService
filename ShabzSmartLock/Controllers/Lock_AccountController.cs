using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShabzSmartLock.Model;

namespace ShabzSmartLock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lock_AccountController : ControllerBase
    {
        private const string Conn =
            "Data Source=gruppe-3.database.windows.net;Initial Catalog=lager;User ID=goldmann;Password=Doodlejump123";
        private static List<Lock_Account> LAList = new List<Lock_Account>();

        // GET: api/Lock_Account
        [HttpGet]
        public List<Lock_Account> GetLockAccount()
        {
            LAList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_role", dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lock_Account la = new Lock_Account(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));
                            
                            LAList.Add(la);
                        }
                    }
                }
            }
            return LAList;
        }

        // GET: api/Lock_Account/5
        [HttpGet("{id}")]
        public Lock_Account Get(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_lock_account WHERE Id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lock_Account la = new Lock_Account(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), Convert.ToInt32(reader[2]), Convert.ToInt32(reader[3]));

                            return la;
                        }
                    }
                }
            }
            return null;
        }

        // POST: api/Lock_Account
        [HttpPost]
        public void Post(Lock_Account la)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("INSERT INTO shabz_lock_account (AccountId, LockId, RoleId) VALUES (@accountId, @lockId, @roleId)", dbConnection))
                {
                    command.Parameters.AddWithValue("@accountId", la.AccountId);
                    command.Parameters.AddWithValue("@lockId", la.LockId);
                    command.Parameters.AddWithValue("@roleId", la.RoleId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Lock_Account/5
        [HttpPut("{id}")]
        public void Put(int id, Lock_Account la)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE shabz_lock_account SET AccountId = @accountId, LockId = @lockId, RoleId = @roleId WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@accountId", la.AccountId);
                    command.Parameters.AddWithValue("@lockId", la.LockId);
                    command.Parameters.AddWithValue("@roleId", la.RoleId);

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

                using (SqlCommand command = new SqlCommand("DELETE from shabz_lock_account WHERE Id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
