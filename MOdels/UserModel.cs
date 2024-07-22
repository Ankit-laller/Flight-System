namespace Flight_System.MOdels
{
    public class UserModel
    {
        public string userId { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public string user {  get; set; }
        public bool isParent { get; set; }

        public string parentId { get; set; }

    }
    public class UserLoginDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
    public class UserRegistrationDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string userType  { get; set; }
/*        public bool isParent { get; set; }
*/    }
    public class SubUserRegistrationDto
    {
        public string email { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string userType { get; set; }
        public string parentId { get; set; }

    }

    /* public enum UserType
     {
     Airline,
     Forwarder
     }*/
}
