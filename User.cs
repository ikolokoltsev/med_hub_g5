
/*
Work to be done
We need to have a user class with fields:

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

To be able to create a new user with full name, date of birth, email, password.
To check if the user already exists by email and password.
Show the list of the users by their role.
Hard code BIG ADMIN.
Nice to have
Registration via SSN.
Have a menu for login and log out.
*/






namespace App;

public class User
{
    public string FirstName;
    public string LastName;
    public int DateOfBirth;
    public int SocialSecurityNumber;
    public string Email;
    public string _password;
    public string Role;

    public User(string firstName, string lastName, int dateOfBirth, int socialSecurityNumber, string email, string password, string role)
    {




        public string Username;
    public string Password;

    public Users(string username, string password)
    {
        Username = username;
        Password = password;
    }
    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }





    FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        SocialSecurityNumber = socialSecurityNumber;
        Email = email;
        _password = password;
        Role = role;

    }



    public bool TryLogin(string email, string password)
    {
        return email == Email && password == _password;
    }

    public string ToSaveString()
    {
        return $"{Email} , {_password}";
    }

}


