namespace aula08.controller;

public class UserController
{
    public string UserLogin { get; set; }
    public string UserPassword { get; set; }

    public UserController(string userLogin, string userPassword)
    {
        UserLogin = userLogin;
        UserPassword = userPassword;
    }

    public bool Login(String user, String password)
    {
        bool response = false;
        if (UserLogin == "admin" && UserPassword == "abc")
        {
            return true;
        }
        return response;
    }
}