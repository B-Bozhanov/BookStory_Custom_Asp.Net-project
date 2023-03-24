namespace BookStory.Services
{
    using BookStory.Data.Data;
    using BookStory.Data.Models;

    public class BookService : IBookService
    {
        private readonly IRepository repository;

        public BookService(IRepository repository)

        {
            this.repository = repository;
        }

        public void Add(string title, string authorFirstName, string authorLastName, string genre, 
                        byte[] audioRecord, string duration, string writedOn)
        {
            var author = this.repository.GetAuthorByName(authorFirstName, authorLastName);

            author ??= new Author { FirstName = authorFirstName, LastName = authorLastName};

            var dbGenre = this.repository.GetGenre(genre);

            dbGenre ??= new Genre { Name = genre };

            var book = new Book
            {
                Title = title,
                Author = author,
                Genre = dbGenre,
                AudioFile = audioRecord,
                Duration = duration,
                WritedOn = DateTime.Parse(writedOn)
            };

            dbGenre.Books.Add(book);
            author.Books.Add(book);

            this.repository.AddBook(book);
            this.repository.AddGenre(dbGenre);

            this.repository.SaveChanges();
        }

        public IEnumerable<Book> GetByRating(double rating)
            => this.repository.GetByBooksRating(rating);

        public Book GetByTitle(string title)
        {
            var book = this.repository.GetBookByName(title);

            if (book == null)
            {
                throw new ArgumentException("The book is not found !");
            }

            return book;
        }
    }
}
