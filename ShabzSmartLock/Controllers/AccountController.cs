using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
        private const string Conn =
            "Data Source=gruppe-3.database.windows.net;Initial Catalog=lager;User ID=goldmann;Password=Doodlejump123";
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
            AccountList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_account", dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AccountList.Add(new Account(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3])));
                        }
                    }
                }
            }

            return AccountList;
        }

        // GET: api/Account/1
        [HttpGet]
        [Route("{input}")]
        public Account GetAccount(string input)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();
                if (int.TryParse(input, out int lockId))
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_account WHERE id = @id", dbConnection))
                    {
                        command.Parameters.AddWithValue("@id", Convert.ToInt32(input));
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Account lås = new Account(Convert.ToInt32(reader[0]), reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3]));

                                return lås;
                            }
                        }
                    }
                }
                else
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_account WHERE email = @email", dbConnection))
                    {
                        command.Parameters.AddWithValue("@email", input);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Account lås = new Account(reader[1].ToString(), reader[2].ToString(), Convert.ToInt32(reader[3]));

                                return lås;
                            }
                        }
                    }
                }

                return null;
            }
        }

        // GET: api/Account/5
        [HttpGet("{id}/logs", Name = "GetAccountsLogs")]
        public List<Log> GetAccountsLogs(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_log WHERE AccountId = @accountId", dbConnection))
                {
                    command.Parameters.AddWithValue("@accountId", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LogController.LogList.Add(new Log(Convert.ToInt32(reader[0]), Convert.ToInt32(reader[1]), reader[2].ToString(), Convert.ToBoolean(reader[3])));
                        }
                    }
                }
            }
            return LogController.LogList;
        }

        // GET: api/Account
        [HttpGet("logs/statistik")]
        public void GetStatistic(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT AccountId, COUNT(*) FROM shabz_log GROUP BY AccountId", dbConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // POST: api/Account
        [HttpPost]
        public void Post(Account ac)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO shabz_account (email, name, PrimaryLockId) VALUES (@email, @name, @primaryLockId)", dbConnection))
                {
                    command.Parameters.AddWithValue("@email", ac.Email);
                    command.Parameters.AddWithValue("@name", ac.Name);
                    command.Parameters.AddWithValue("@primaryLockId", ac.PrimaryLockId);
               
                    command.ExecuteNonQuery();

                    AccountList.Add(new Account(ac.Email, ac.Name, ac.PrimaryLockId));
                }
            }
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put(int id, string name, string email, int primaryLock)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("UPDATE shabz_account SET email = @email, name = @name, PrimaryLockId = @primaryLockId WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.Add("@email", SqlDbType.Text);
                    command.Parameters.Add("@name", SqlDbType.Text);
                    command.Parameters.Add("@primaryLockId", SqlDbType.Int);

                    command.ExecuteNonQuery();
                    var AccountToUpdate = AccountList.FirstOrDefault(u => u.Id == id);
                    AccountToUpdate.Name = name;
                    AccountToUpdate.Email = email;
                    AccountToUpdate.PrimaryLockId = primaryLock;
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


                using (SqlCommand command = new SqlCommand("DELETE from shabz_account WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();

                    var AccountToDelete = AccountList.FirstOrDefault(u => u.Id == id);
                    AccountList.Remove(AccountToDelete);
                }
            }
        }
    }
}
