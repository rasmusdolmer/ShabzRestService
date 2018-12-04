using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShabzSmartLock.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AccessLevel { get; set; }
        public string Icon { get; set; }
        private static int NextId = 1;

        public Role()
        {
            
        }

        public Role(string name, int accessLevel, string icon)
        {
            Id = NextId++;
            Name = name;
            AccessLevel = accessLevel;
            Icon = icon;
        }

        public Role(int id, string name, int accessLevel, string icon)
        {
            Id = id;
            Name = name;
            AccessLevel = accessLevel;
            Icon = icon;
        }
    }
}
