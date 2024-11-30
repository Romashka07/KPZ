namespace Lab3KPZ_CF_.ViewModels
{
    public class UserRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public int RoleID { get; set; }
    }

    public class UserLoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
