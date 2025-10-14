
/*
Work to be done
We need ot have a user class with fields:

Full name
Date of birth
SSN(optional)
Email
_password
Role
The Role should be an enum:

Patient
Personal
Admin
The main functionality:

To be able to crate a new user with full name, date of birth, email, password.
To check if the user already exists by email and password.
Show the list of the users by their role.
Hard code BIG ADMIN.
Nice to have
Registration via SSN.
Have a menu for login and log out.
*/






namespace User
{
    // Class for Users

    public class Users
    {
        public string Username;
        public string Password;

        public Users(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}

