namespace BookStory.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.CompilerServices;

    public class Book
    {
        public Book() 
            => this.Accounts = new HashSet<Account>();

        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public double Rating { get; set; }

        public string Duration { get; set; }

        public DateTime WritedOn { get; set; }

        public int NumberOfReads { get; set; }

        [Required]
        public byte[] AudioFile { get; set; }

        public int AuthorId { get; set; }

        public Author Author { get; set; }

        public int GenreId { get; set; }

        public Genre Genre { get; set; }

        public ICollection<Account> Accounts { get; set; }

        public void Play()
        {
            //File.ReadAllText(" "); //TODO: 

            this.IsListened = true;
        }

        public bool IsListened { get; set; } = false;
    }
}
