namespace TestDataApi.Services.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string username, string password);
    AuthenticationResult Login(string username, string password);
}