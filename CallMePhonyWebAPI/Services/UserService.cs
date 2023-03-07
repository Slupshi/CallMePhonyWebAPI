using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Services;

public class UserService : IUserService
{
    private readonly CallMePhonyDbContext _context;

    public UserService(CallMePhonyDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserAsync(int id) => await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<User?> GetUserByMailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByNameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User?>> GetUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> PostUserAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<User?> PutUserAsync(User model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}
