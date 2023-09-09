using MontyTask.Data.DTOs;

namespace MontyTask.Services;

//if the services are to be scalable,interface would then be in an independent file
public interface IAuthenticationService
{
    Task<TokenResponse> CreateAccessTokenAsync(string email, string password);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenHandler _tokenHandler;
    
    public AuthenticationService( IUserService userService, IPasswordHasher passwordHasher, ITokenHandler tokenHandler)
    {
        _tokenHandler = tokenHandler;
        _passwordHasher = passwordHasher;
        _userService = userService;
    }

    public async Task<TokenResponse> CreateAccessTokenAsync(string email, string password)
    {
        var user = await _userService.FindByEmailAsync(email);

        if (user == null || !_passwordHasher.PasswordMatches(password, user.Password))
        {
            return new TokenResponse(false, "Invalid credentials.", null);
        }

        var token = _tokenHandler.CreateAccessToken(user);

        return new TokenResponse(true, null, token);
    }
}