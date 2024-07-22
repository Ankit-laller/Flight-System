using Flight_System.MOdels;

namespace Flight_System.Utils
{
    
    public class UserLoginResponseClass
    {
        public bool success { get; set; }
        public string message { get; set; }
        public UserData user { get; set; }
        public UserLoginResponseClass(UserData user, bool success=false, string message="") 
        { 
            this.user = user;
            this.success = success;
            this.message = message;
        }

       
    }

    public class UserRegistrationResponse {
        public bool success { get; set; }
        public string message { get; set; }
        public UserRegistrationResponse(bool success, string message)
        {
            this.success = success;
            this.message = message;
        }
    }

    public class UserData
    {
        public string name { get; set; }
        public string userId { get; set; }
        public string userType { get; set; }
        public bool isParent { get; set; }

    }
}
