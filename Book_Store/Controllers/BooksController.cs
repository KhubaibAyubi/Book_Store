using Book_Store.Context;
using Book_Store.Models;
using Book_Store.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Store.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _bookRepository;

        public BooksController()
        {
            _bookRepository = new BookRepository(new BooksContext());
        }
        // GET: Books
        public ActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();
            return View(books);
        }
        // Show the Create form
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Paractice()
        {
            return View();
        }

        // Handle Create form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Books book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(book);
                _bookRepository.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Edit form
        public ActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // Handle Edit form submission
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Books book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book);
                _bookRepository.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Delete confirmation
        public ActionResult Delete(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // Handle Delete confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return RedirectToAction("Index");
        }


    }
}