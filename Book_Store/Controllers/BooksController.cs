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
            // Fetch the filtered and paginated list of books from the repository
            var books = _bookRepository.GetAllBooks(searchTerm, page, pageSize);
            // Get the total count of books for the given search term to calculate pagination
            var totalBooksCount = _bookRepository.GetTotalBooksCount(searchTerm);
            // Create a ViewModel to pass the data to the view
            var viewModel = new BooksViewModel
            {
                Books = books, // List of books to display on the current page
                SearchTerm = searchTerm,  // The search keyword entered by the user
                CurrentPage = page,// The current page number
                TotalPages = (int)Math.Ceiling((double)totalBooksCount / pageSize), // Calculate the total number of pages
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
        [HttpPost] // Specifies that this action method responds to HTTP POST requests
        [Route("books/create")] // Defines the custom route for this action
        [ValidateAntiForgeryToken]  // Protects against Cross-Site Request Forgery (CSRF) attacks
        public ActionResult Create(Books book)
        {
            // Check if the model state is valid (ensures all validation attributes are satisfied)
            if (ModelState.IsValid)
            {
                // Add the new book to the repository
                _bookRepository.AddBook(book);
                // Save the changes to the database
                _bookRepository.Save();
                // Store a success message in TempData to display it on the redirected page
                TempData["SuccessMessage"] = "Book Added Successfully";
                // Redirect to the Index action to display the list of books

                return RedirectToAction("Index");
            }
            return View(book);
        }

        // Show the Edit form
        [Route("books/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            // Fetch the book details using the provided ID
            var book = _bookRepository.GetBookById(id);
            // Check if the book exists in the database
            if (book == null)
            {
                return HttpNotFound();
            }

            // Debugging: Logs the PublishDate of the book for diagnostic purposes
            Debug.WriteLine(book.PublishDate);
            return View(book);
        }

        // Handle Edit form submission
        [HttpPost]
        [Route("books/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Books book)
        {
            // Validate the submitted data against the model's validation rules
            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book);
                _bookRepository.Save();
                // Store a success message in TempData to display it on the redirected page
                TempData["SuccessUpdateMessage"] = "Book Updated Successfully";

                return RedirectToAction("Index");
            }
            // If validation fails, return the Edit view with the current book data
            // This ensures the user's input is preserved, and validation errors are displayed

            return View(book);
        }

       


        // Handle Delete confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            // Deletes the book from the repository using the provided ID
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return RedirectToAction("Index");
        }


    }
}