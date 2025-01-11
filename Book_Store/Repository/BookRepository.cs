using Book_Store.Context;
using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Book_Store.Repository
{
    public class BookRepository : IBooksRepository
    {
        private readonly BooksContext _context;
        public BookRepository(BooksContext context)
        {
            _context = context;
        }
        public void AddBook(Books book)
        {
            // Adds the book entity to the Books DbSet in the context
            _context.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            // Finds the book entity by its ID
            var book = _context.Books.Find(id);
            // If the book exists, removes it from the DbSet
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
        // Retrieves a list of books with optional search and pagination
        public IEnumerable<Books> GetAllBooks(string searchTerm, int page, int pageSize)
        {
            // Starts querying the Books DbSet
            var query = _context.Books.AsQueryable();

            // Apply search term filtering if provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }

            // Apply pagination and sorting by Title (or another property as needed)
            var books = query
                        .OrderBy(b => b.Id)  // Can be replaced by any other sorting criteria
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            return books;
        }
        // Retrieves a specific book by its ID
        public Books GetBookById(int id)

        {
            // Finds and returns the book entity by ID
            return _context.Books.Find(id);
        }

        public void Save()
        {
            // Commits all changes tracked by the DbContext to the database
            _context.SaveChanges();
        }

        public void UpdateBook(Books book)
        {
            // Marks the book entity as modified in the DbContext
            _context.Entry(book).State = EntityState.Modified;
        }
        // Returns the total number of books that match the search term
        public int GetTotalBooksCount(string searchTerm)
        {
            // Starts querying the Books DbSet
            var query = _context.Books.AsQueryable();

            // Filters books based on the search term if it's provided
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }
            // Returns the count of the filtered query
            return query.Count();
        }
    }
}
