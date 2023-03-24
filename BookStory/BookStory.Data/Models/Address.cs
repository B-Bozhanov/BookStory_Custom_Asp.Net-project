namespace BookStory.Data.Models
{
    public class Address
    {
        public Address()
        {
            this.Accounts = new HashSet<Account>();
        }

        public int Id { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Street { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}