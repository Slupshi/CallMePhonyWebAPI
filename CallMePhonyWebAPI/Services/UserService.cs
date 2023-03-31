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

    public async Task<User?> GetUserAsync(int id) => await _context.Users.Include(u => u.Service)
                                                                         .Include(u => u.Site)
                                                                         .FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetUserByMailAsync(string email) => await _context.Users.Include(u => u.Service)
                                                                                     .Include(u => u.Site)
                                                                                     .FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<User?>> GetUsersByNameAsync(string username) => await _context.Users.Include(u => u.Service)
                                                                                                      .Include(u => u.Site)
                                                                                                      .Where(u => u.UserName.Contains(username))
                                                                                                      .ToListAsync();

    public async Task<IEnumerable<User?>> GetUsersBySiteAsync(Site site) => await _context.Users.Include(u => u.Service)
                                                                                                .Include(u => u.Site)
                                                                                                .Where(u => u.Site == site)
                                                                                                .ToListAsync();

    public async Task<IEnumerable<User?>> GetUsersByServiceAsync(Service service) => await _context.Users.Include(u => u.Service)
                                                                                                         .Include(u => u.Site)
                                                                                                         .Where(u => u.Service == service)
                                                                                                         .ToListAsync();

    public async Task<IEnumerable<User?>> GetUsersAsync() => await _context.Users.Include(u => u.Service)
                                                                                 .Include(u => u.Site)
                                                                                 .ToListAsync();

    public async Task<User?> PostUserAsync(User model)
    {
        User? newUser = (await _context.Users.AddAsync(model)).Entity;
        await _context.SaveChangesAsync();
        return newUser;
    }

    public async Task<User?> PutUserAsync(User model)
    {
        //User? dbUser = await GetUserAsync(model.Id);
        //dbUser.UserName = model.UserName;
        //dbUser.Email = model.Email;
        //dbUser.Password = model.Password;
        //dbUser.UserType = model.UserType;
        //dbUser.Site = model.Site;
        //dbUser.Service = model.Service;
        //dbUser.LastName = model.LastName;
        //dbUser.FirstName = model.FirstName;
        //dbUser.MobilePhone = model.MobilePhone;
        //dbUser.Phone = model.Phone;
        //await _context.SaveChangesAsync();
        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        User? dbUser = await GetUserAsync(id);
        if(dbUser != null)
        {
            _context.Users.Remove(dbUser);
            return true;
        }
        return false;
    }


}
