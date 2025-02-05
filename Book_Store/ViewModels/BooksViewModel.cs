﻿using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Store.ViewModels
{
    public class BooksViewModel
    {
        public IEnumerable<Books> Books { get; set; }
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}