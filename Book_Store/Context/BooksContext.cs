using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Book_Store.Context
{
    // This class represents the database context for the Book_Store application
    public class BooksContext : DbContext
    {
        // DbSet represents a collection of Books in the database
        public DbSet<Books> Books { get; set; }
        // Constructor for the EmployeeContext class, passing the connection string to the base class
        public BooksContext() : base("BookstoreConnection")
        { 
        }

    }
}