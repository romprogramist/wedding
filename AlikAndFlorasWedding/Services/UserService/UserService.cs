using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AlikAndFlorasWedding.Data;
using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AlikAndFlorasWedding.Services.UserService;

public class UserService : IUserService
{
    private readonly DataContext _context;
    

    public UserService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<bool> RegisterUserAsync(UserDto userDto)
    {
        if (_context.Users.Any(u => u.UserName == userDto.UserName && u.Role == userDto.Role))
        {
            return false;
        }
        
        var user = new User
        {
            UserName = userDto.UserName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Role = userDto.Role
        };
        
        await _context.Users.AddAsync(user);
        var savedCount = await _context.SaveChangesAsync();
        return savedCount > 0;
    }

    public Task<UserDto?> GetUserAsync(string userName, string role)
    {
        return _context.Users
            .Where(u => u.UserName == userName && u.Role == role)
            .Select(u => new UserDto
            {
                UserName = u.UserName,
                PasswordHash = u.PasswordHash,
                Role = u.Role
            })
            .SingleOrDefaultAsync();
    }
}