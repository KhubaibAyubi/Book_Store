using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Store.Repository
{
    public interface IBooksRepository
    {
        IEnumerable<Books> GetAllBooks(string searchTerm, int page, int pageSize);
        Books GetBookById(int id);
        void AddBook(Books book);
        void UpdateBook(Books book);
        void DeleteBook(int id);
        void Save();
        // New method to get the total count of books based on search term
        int GetTotalBooksCount(string searchTerm); // For pagination purposes
    }
}
