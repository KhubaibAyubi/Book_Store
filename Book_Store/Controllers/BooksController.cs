using Book_Store.Context;
using Book_Store.Models;
using Book_Store.Repository;
using Book_Store.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public ActionResult Index(string searchTerm, int page = 1, int pageSize = 5)
        {
            var books = _bookRepository.GetAllBooks(searchTerm, page, pageSize);

            var totalBooksCount = _bookRepository.GetTotalBooksCount(searchTerm);

            var viewModel = new BooksViewModel
            {
                Books = books,
                SearchTerm = searchTerm,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)totalBooksCount / pageSize),
            };

            return View(viewModel);
        }
        // Show the Create form
        [Route("books/create")]
        public ActionResult Create()
        {
            return View();
        }


        // Handle Create form submission
        [HttpPost]
        [Route("books/create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Books book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(book);
                _bookRepository.Save();

                TempData["SuccessMessage"] = "Book Added Successfully";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Edit form
        [Route("books/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            // Debugging the value
            Debug.WriteLine(book.PublishDate);
            return View(book);
        }

        // Handle Edit form submission
        [HttpPost]
        [Route("books/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Books book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book);
                _bookRepository.Save();
                TempData["SuccessUpdateMessage"] = "Book Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Delete confirmation


        // Handle Delete confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return RedirectToAction("Index");
        }


    }
}