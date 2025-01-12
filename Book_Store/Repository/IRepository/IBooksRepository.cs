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
        // Retrieves all books with optional search term, pagination, and page size
        IEnumerable<Books> GetAllBooks(string searchTerm, int page, int pageSize);
        // Retrieves a specific book by its unique ID
        Books GetBookById(int id);
        // Adds a new book to the repository
        void AddBook(Books book);
        // Updates an existing book in the repository
        void UpdateBook(Books book);
        // Deletes a book from the repository by its ID
        void DeleteBook(int id);
        // Saves changes made to the repository (commits to the database)
        void Save();
        // Retrieves the total count of books based on the search term
        // Useful for pagination to calculate total pages
        int GetTotalBooksCount(string searchTerm); // For pagination purposes
    }
}
