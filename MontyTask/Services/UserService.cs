using MontyTask.Data;
using MontyTask.Data.DTOs;
using MontyTask.Data.Models;
using MontyTask.Data.Repositories;

namespace MontyTask.Services;

//if the services are to be scalable,interface would then be in an independent file
public interface IUserService
{
    Task<CreateUserResponse> CreateUserAsync(User user);
    Task<User> FindByEmailAsync(string email);
}

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService( IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<CreateUserResponse> CreateUserAsync(User user)
    {
        var existingUser = await _userRepository.FindByEmailAsync(user.Email);

        if (existingUser != null)
        {
            return new CreateUserResponse(false, "Email already in use.", null);
        }

        user.Password = _passwordHasher.HashPassword(user.Password);

        await _userRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        return new CreateUserResponse(true, null, user);
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        return await _userRepository.FindByEmailAsync(email);
    }
}