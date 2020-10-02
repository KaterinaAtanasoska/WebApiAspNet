using Class02Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class02Homework
{
    public static class StaticDb
    {
        public static List<string> UserNames = new List<string>
        {
            "user1",
            "user2",
            "user3"
        };

        public static List<User> User = new List<User>()
        {
            new User(){ 
                Id = 1, 
                FirstName = "Name1", 
                LastName = "Last1"
            },
            new User(){
                Id = 2,
                FirstName = "Name2",
                LastName = "Last2"
            },
            new User(){
                Id = 3,
                FirstName = "Name3",
                LastName = "Last3"
            }

        };
    }
}
