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

        public IEnumerable<Books> GetAllBooks()
        {
            return _context.Books.ToList();
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
    }
}