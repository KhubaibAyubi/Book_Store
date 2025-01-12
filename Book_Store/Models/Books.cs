using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
    
namespace Book_Store.Models
{
    public class Books
    {
        // Primary Key for the Books table
        [Key]
        public int Id { get; set; }

        // Title of the book - Required with a maximum length of 100 characters
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }
        // Author of the book - Required with a maximum length of 50 characters

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(50, ErrorMessage = "Author name cannot exceed 50 characters.")]
        public string Author { get; set; }

        // Price of the book - Required and must be between 0.01 and 999.99
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 999.99, ErrorMessage = "Price must be between 0.01 and 999.99.")]
        public decimal Price { get; set; }

        // Publish Date of the book - Required and must be a valid date
        [Required(ErrorMessage = "Publish Date is required.")]
        [DataType(DataType.Date)] // Ensures that this field is treated as a date in forms
        [Display(Name = "Publish Date")]
        [CustomValidation(typeof(Books), nameof(ValidatePastDate))]
        public DateTime PublishDate { get; set; }

        // ISBN of the book - Required and has a maximum length of 13 characters
        [Required(ErrorMessage = "ISBN is required.")]
        [StringLength(13, ErrorMessage = "ISBN cannot exceed 13 characters.")]
        public string ISBN { get; set; }

        public static ValidationResult ValidatePastDate(DateTime date, ValidationContext context)
        {
            if (date > DateTime.Now)
            {
                return new ValidationResult("Publish Date must be in the past.");
            }
            return ValidationResult.Success;
        }
    }

}