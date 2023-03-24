namespace BookStory.Data.Data
{
    using BookStory.Data.Models;

    public interface IRepository 
    {
        public void AddBook(Book book);

        public void AddBooksRange(IEnumerable<Book> books);

        public void AddGenre(Genre genre);

        public void AddAuthor(Author author);

        public void AddAccount(Account account);

        public Book GetBookByName(string name);

        public Genre GetGenre(string name);

        public bool IsExistEmail(string email);

        public bool IsExistUsername(string username);

        public Author GetAuthorByName(string firstName, string lastName);

        public Account GetAccountByEmail(string email, string password);

        public Account GetAccountByUsername(string username, string password);

        public IEnumerable<Book> GetByBooksRating(double rating);

        public IEnumerable<Book> GetByAuthorRating(double rating);

        public IEnumerable<Book> GetBooksByAuthorName(string authorFirstName, string authorLastName);

        public void Migration();

        public void SaveChanges();

    }
}
