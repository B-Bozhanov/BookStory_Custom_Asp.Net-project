namespace BookStory.Services
{
    using BookStory.Data.Models;

    public interface IBookService
    {
        public void Add(string title, string authorFirstName, string authorLastName, string genre, 
                        byte[] audioRecord, string duration, string writedOn);

        public Book GetByTitle(string title);

        public IEnumerable<Book> GetByRating(double rating);
    }
}
