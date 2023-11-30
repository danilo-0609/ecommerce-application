namespace Ecommerce.API.Modules.UserAccess;

public struct RegisterNewUserRequest
{
    public string Login { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public RegisterNewUserRequest(string login, 
        string password, 
        string email, 
        string firstName, 
        string lastName = "")
    {
        Login = login;
        Password = password;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }
}
