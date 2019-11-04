using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student_Management.BusinessLayer;

namespace Student_Management_MVC.Models
{
    //Connects the model objects to the database.
    public class Student_Management_MVCContext : DbContext
    {
        public Student_Management_MVCContext (DbContextOptions<Student_Management_MVCContext> options)
            : base(options)
        {
        }

        public DbSet<Student_Management.BusinessLayer.Course> Course { get; set; }

        public DbSet<Student_Management.BusinessLayer.Enrollment> Enrollment { get; set; }

        public DbSet<Student_Management.BusinessLayer.Result> Result { get; set; }

        public DbSet<Student_Management.BusinessLayer.Student> Student { get; set; }
    }
}
