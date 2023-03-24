namespace BookStory.Services
{
    using System.Net.Mail;
    using System.Security.Cryptography;
    using System.Text;

    using BookStory.Data.Data;
    using BookStory.Data.Models;
    using BookStory.Services.Common;

    public class AccountService : IAccountService
    {
        private const int SaltBytesLength = 64;

        private readonly IRepository repository;
        private int verificationCode;
        private DateTime emailSentTime;
        private Random random;


        public AccountService(IRepository repository)
        {
            this.repository = repository;
        }


        public Account CreateAccount(string firstName, string lastName, int years, string username, string encyptedPassword, string email)
        {
            this.EmailValidator(email); // TODO: Its inpossible to create two equals emails on one email provider, may be it is not necessary validation!
            this.UsernameValidator(username);

            var account = new Account
            {
                FirstName = firstName,
                LastName = lastName,
                Years = years,
                Username = username,
                Password = encyptedPassword,
                Email = email
            };


            this.random = new Random();

            this.verificationCode = this.random.Next(100000, 999999);
            //TODO: EmailSender.SentTo(email, this.verificationCode);
            //TODO: EmailSender.SentTime = DateTime.Now
            //TODO: this.emailSentTime = EmailSender.SentTime;

            this.repository.AddAccount(account);

            this.repository.SaveChanges();

            return account;
        }

        public Account Login(string username, string encryptedPassword)
        {
            var account = this.repository.GetAccountByUsername(username, encryptedPassword);

            if (account == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAccount);
            }

            return account;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public void EmailVerification(int verificationCode)
        {
            var expiredTime = (DateTime.Now - this.emailSentTime).Minutes; //TODO: Check it is true!

            if (expiredTime > GlobalConstants.VerificationTimeMinutes)
            {
                throw new ArgumentException(ExceptionMessages.ExpiredTime);
            }

            if (this.verificationCode != verificationCode)
            {
                throw new ArgumentException(ExceptionMessages.WrongVerificationCode);
            }
        }

        private void EmailValidator(string email)
        {
            if (this.repository.IsExistEmail(email))
            {
                throw new ArgumentException(ExceptionMessages.EmailExist);
            }

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException(ExceptionMessages.InvalidEmail);
            }
        }

        private void UsernameValidator(string username)
        {
            if (this.repository.IsExistUsername(username))
            {
                throw new ArgumentException(ExceptionMessages.UsernameExist);
            }
        }

        public void PasswordValidator(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(ExceptionMessages.PasswordCanotBeEmpty);
            }

            if (!GlobalConstants.IsValidPassword(password))
            {
                throw new ArgumentException(ExceptionMessages.PasswordRequirements);
            }
        }

        public string EncryptPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException(ExceptionMessages.PasswordCanotBeEmpty);
            }

            var sha256 = SHA256.Create();

            var salt = SaltGenerator();

            var saltedPassword = string.Format("{0}{1}", salt, password);
            byte[] saltedPasswordAsBytes = Encoding.UTF8.GetBytes(saltedPassword);

            var hashedPasswordBytes = sha256.ComputeHash(saltedPasswordAsBytes);

            var encryptedPassword = Convert.ToBase64String(hashedPasswordBytes);

            return encryptedPassword;
        }

        private static string SaltGenerator()
        {
            var saltMessage = GlobalConstants.SaltTeaxt;

            var saltBytes = new byte[saltMessage.Length];

            saltBytes = Encoding.UTF8.GetBytes(saltMessage);

            return Convert.ToBase64String(saltBytes);
        }
    }
}
