namespace BookStory.Services
{
    using BookStory.Data.Models;

    public interface IAccountService
    {
        public Account CreateAccount(string firstName, string lastName, int years, string userName, string password, string email);

        public void EmailVerification(int verificationCode);

        public Account Login(string username, string password);

        public void PasswordValidator(string password);

        public string EncryptPassword(string password);

        public void Logout();
    }
}
