using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private static int NextId = 1;

        public User()
        {
            
        }

        public User(string name)
        {
            Id = NextId++;
            Name = name;
        }
    }
}
