namespace BookStory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class Account
    {
        public Account()
        {
            this.Books = new HashSet<Book>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        public int Years { get; set; }

        [Required]
        [MaxLength(30)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsVIP { get; set; }

        public string? PhoneNumber { get; set; }

        public int? AddressId { get; set; }

        public Address? Address { get; set; }

        public int? BooksCount { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}