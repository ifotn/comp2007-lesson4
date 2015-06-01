using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data.Entity;

namespace lesson4.Models
{
    //class to interact with the database
    public class StudentContext : DbContext
    {
        public StudentContext()
            : base("ContosoEntities")
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}