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

    public Task<User?> GetUsersByNameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUsersBySiteAsync(Site site)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUsersByServiceAsync(Service service)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<User?>> GetUsersAsync() => await _context.Users.Include(u => u.Service)
                                                                                 .Include(u => u.Site)
                                                                                 .ToListAsync();

    public async Task<User?> PostUserAsync(User model)
    {
        User? newUser = (await _context.Users.AddAsync(model)).Entity;
        await _context.SaveChangesAsync();
        return newUser;
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
