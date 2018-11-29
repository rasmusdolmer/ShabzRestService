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
    public class UserController : ControllerBase
    {
        private static List<User> UserList = new List<User>()
        {
            new User("Frederik"),
            new User("Kian"),
            new User("Lucas"),
            new User("Marcus"),
            new User("Rasmus")
        };

        // GET: api/User
        [HttpGet]
        public List<User> Get()
        {
            return UserList;
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public User GetUser(int id)
        {
            var user = UserList.FirstOrDefault(l => l.Id == id);

            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }

        // GET: api/User/5
        [HttpGet("{id}/locks", Name = "GetUsersLocks")]
        public Lock GetUsersLocks(int id)
        {
            var userLock = LockController.LockList.FirstOrDefault(l => l.UserId == id);

            if (userLock != null)
            {
                return userLock;
            }
            else
            {
                throw new Exception("Denne bruger har ingen tilknyttede locks");
            }
        }

        // GET: api/User/5
        [HttpGet("{id}/logs", Name = "GetUsersLogs")]
        public List<Log> GetUsersLogs(int id)
        {
            var userLogs = (from log in LogController.LogList where log.UserId == id select log).ToList();

            if (userLogs.Count > 0)
            {
                return userLogs;
            }
            else
            {
                throw new Exception("Denne bruger har ingen tilknyttede logs");
            }
        }

        // POST: api/User
        [HttpPost]
        public void Post(User u)
        {
            UserList.Add(new User(u.Name));
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, string name)
        {
            var userToUpdate = UserList.FirstOrDefault(u => u.Id == id);
            if (userToUpdate != null)
            {
                userToUpdate.Name = name;
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var userToDelete = UserList.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                UserList.Remove(userToDelete);
            }
            else
            {
                throw new Exception("Der findes ikke en bruger med dette id.");
            }
        }
    }
}
