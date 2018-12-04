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
    public class RoleController : ControllerBase
    {
        private const string Conn =
            "Data Source=gruppe-3.database.windows.net;Initial Catalog=lager;User ID=goldmann;Password=Doodlejump123";
        private static List<Role> RoleList = new List<Role>();

        // GET: api/Role
        [HttpGet]
        public List<Role> Get()
        {
            RoleList.Clear();
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_role", dbConnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role role = new Role(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), reader[3].ToString());

                            RoleList.Add(role);
                        }
                    }
                }
            }
            return RoleList;
        }

        // GET: api/Role/5
        [HttpGet("{id}")]
        public Role Get(int id)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM shabz_role WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Role role = new Role(Convert.ToInt32(reader[0]), reader[1].ToString(), Convert.ToInt32(reader[2]), reader[3].ToString());

                            return role;
                        }
                    }
                }
            }
            return null;
        }

        // POST: api/Role
        [HttpPost]
        public void Post(Role role)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();


                using (SqlCommand command = new SqlCommand("INSERT INTO shabz_role (name, accessLevel, icon) VALUES (@name, @accessLevel, @icon)", dbConnection))
                {
                    command.Parameters.AddWithValue("@name", role.Name);
                    command.Parameters.AddWithValue("@accessLevel", role.AccessLevel);
                    command.Parameters.AddWithValue("@icon", role.Icon);

                    command.ExecuteNonQuery();
                }
            }
        }

        // PUT: api/Role/5
        [HttpPut("{id}")]
        public void Put(int id, Role role)
        {
            using (SqlConnection dbConnection = new SqlConnection(Conn))
            {
                dbConnection.Open();

                using (SqlCommand command = new SqlCommand("UPDATE shabz_role SET name = @name, accessLevel = @accessLevel, icon = @icon WHERE id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", role.Name);
                    command.Parameters.AddWithValue("@accessLevel", role.AccessLevel);
                    command.Parameters.AddWithValue("@icon", role.Icon);

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

                using (SqlCommand command = new SqlCommand("DELETE from shabz_role WHERE Id = @id", dbConnection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
