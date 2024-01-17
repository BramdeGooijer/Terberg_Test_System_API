using TestDataApi.Interfaces.Authentication;

namespace TestDataApi.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _tokenGenerator;

    public AuthenticationService(IJwtTokenGenerator tokenGenerator)
    {
        _tokenGenerator = tokenGenerator;
    }

    public AuthenticationResult Register(string username, string password)
    {
        // Check if user exists
        // Create user unique id
        //Create JWT token

        var userId = Guid.NewGuid();
        var token = _tokenGenerator.GenerateToken(userId, username);
        
        return new AuthenticationResult(userId, username, token);
    }
    
    public AuthenticationResult Login(string username, string password)
    {
        var userId = Guid.NewGuid();
        var token = _tokenGenerator.GenerateToken(userId, username);
        
        return new AuthenticationResult(userId, username, token);
    }
}