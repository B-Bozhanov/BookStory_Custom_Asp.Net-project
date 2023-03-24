namespace BookStory.Data.Data
{
    using BookStory.Data.Models;

    using Microsoft.EntityFrameworkCore;

    public class Repository : IRepository
    {
        private readonly ApplicationDbContext dbContext;

        public Repository()
           => this.dbContext = new ApplicationDbContext();

        public void AddBook(Book book)
           => this.dbContext.Books.Add(book);

        public void AddBooksRange(IEnumerable<Book> books)
            => this.dbContext.Books.AddRange(books);

        public void AddGenre(Genre genre)
            => this.dbContext.Genres.Add(genre);

        public void AddAuthor(Author author)
            => this.dbContext.Authors.Add(author);

        public void UpdateAuthor(Author author)
            => this.dbContext.Authors.Update(author);

        public Book GetBookByName(string name)
            => this.dbContext.Books.FirstOrDefault(b => b.Title == name)!;

        public IEnumerable<Book> GetBooksByAuthorName(string firstName, string lastName)
            => this.dbContext.Books.Where(b => b.Author.FirstName == firstName && b.Author.LastName == lastName);

        public IEnumerable<Book> GetByAuthorRating(double rating)
            => this.dbContext.Books.Where(b => b.Author.Rating >= rating);

        public IEnumerable<Book> GetByBooksRating(double rating)
            => this.dbContext.Books.Where(b => b.Rating >= rating)
                                   .OrderByDescending(b => b.Rating);

        public void UpdateBook(Book book)
           => this.dbContext.Books.Update(book);

        public void SaveChanges()
           => this.dbContext.SaveChanges();

        public void Migration()
           => this.dbContext.Database.Migrate();

        public Author GetAuthorByName(string firstName, string lastName)
            => this.dbContext.Authors.FirstOrDefault(a => a.FirstName == firstName && a.LastName == lastName)!;

        public Genre GetGenre(string name)
            => this.dbContext.Genres.FirstOrDefault(g => g.Name == name)!;

        public bool IsExistEmail(string email)
            => this.dbContext.Accounts.Any(a => a.Email == email);

        public bool IsExistUsername(string username)
            => this.dbContext.Accounts.Any(a => a.Username == username);

        public Account GetAccountByEmail(string email, string password)
            => this.dbContext.Accounts.FirstOrDefault(a => a.Email == email && a.Password == password)!;

        public Account GetAccountByUsername(string username, string password)
             => this.dbContext.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password)!;

        public void AddAccount(Account account)
            => this.dbContext.Accounts.Add(account);
    }
}
