using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MontyTask.Data.Models;
using MontyTask.Data.Resources;
using MontyTask.Helpers;
using MontyTask.Services;

namespace MontyTask.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(IUserService userService, IMapper mapper, ILogger<UsersController> logger)
    {
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(
    [FromBody] UserCredentialsResource userCredentials)
    {
        try
        {
            var user = _mapper.Map<UserCredentialsResource, User>(userCredentials);

            var response = await _userService.CreateUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            var userResource = _mapper.Map<UserResource>(response.User);

            _logger.LogWarning($"User created successfully under username : {userCredentials.Email}");
            return Ok(userResource);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ExtractMessage());
            return StatusCode(500, "Internal server error");
        }
    }


}
