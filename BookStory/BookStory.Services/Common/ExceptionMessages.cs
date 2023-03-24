namespace BookStory.Services.Common
{
    public static class ExceptionMessages
    {
        public const string UsernameExist = "The username already exist !";
        public const string EmailExist = "The user already exist !";
        public const string WrongUsernameOrPassword = "Invalid username or password!";
        public const string WrongVerificationCode = "Invalid verificatin code !";
        public const string ExpiredTime = "The verification time is expired, try again !";
        public const string PasswordRequirements = "The password must contains at least one upper letter, and at least one Lower, and at least one digit, and must be longer by five symbols ! ";
        public const string InvalidAccount = "Invalid username or password !";
        public const string InvalidEmail = "Invalid email address format !";
        public const string PasswordCanotBeEmpty = "Password can not be empty !";
    }
}
