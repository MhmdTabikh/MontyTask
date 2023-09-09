using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MontyTask.Data.DTOs;
using MontyTask.Data.Resources;
using MontyTask.Helpers;
using MontyTask.Services;

namespace MontyTask.Controllers;

[ApiController]
[Route("api/")]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<AuthController> _logger;
    private readonly IAuthenticationService _authenticationService;

    public AuthController(IMapper mapper, IAuthenticationService authenticationService, ILogger<AuthController> logger)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserCredentialsResource userCredentials)
    {
        try
        {
            var response = await _authenticationService.CreateAccessTokenAsync(userCredentials.Email, userCredentials.Password);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            var accessTokenResource = _mapper.Map<AccessToken, AccessTokenResource>(response.Token);
            _logger.LogWarning($"Login succeeded for user {userCredentials.Email},access token returned");
            return Ok(accessTokenResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ExtractMessage());
            return StatusCode(500, "Internal server error");
        }
    }
}
