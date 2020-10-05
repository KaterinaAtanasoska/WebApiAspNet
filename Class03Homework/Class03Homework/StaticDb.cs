using Class03Homework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Class03Homework
{
    public class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book(){Author="Author1",Title="Title1"},
            new Book(){Author="Author2",Title="Title2"},
            new Book(){Author="Author3",Title="Title3"}
        };
    }
}
