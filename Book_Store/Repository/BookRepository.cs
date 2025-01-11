using Book_Store.Context;
using Book_Store.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            _context.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
        }
        public IEnumerable<Books> GetAllBooks(string searchTerm, int page, int pageSize)
        {
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

        public Books GetBookById(int id)
        {
            return _context.Books.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateBook(Books book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }
        public int GetTotalBooksCount(string searchTerm)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }

            return query.Count();
        }
    }
}
