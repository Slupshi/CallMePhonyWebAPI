using CallMePhonyEntities.Models;
using CallMePhonyWebAPI.Data;
using CallMePhonyWebAPI.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CallMePhonyWebAPI.Services;

public class UserService : IUserService
{
    private readonly CallMePhonyDbContext _context;
    private readonly ISiteService _siteService;
    private readonly IServiceService _serviceService;

    public UserService(CallMePhonyDbContext context, ISiteService siteService, IServiceService serviceService)
    {
        _context = context;
        _siteService = siteService;
        _serviceService = serviceService;
    }

    // </inheritedoc>
    public async Task<User?> GetUserAsync(int id) => await _context.Users.AsNoTracking()
                                                                         .Include(u => u.Service)
                                                                         .Include(u => u.Site)
                                                                         .FirstOrDefaultAsync(u => u.Id == id);

    // </inheritedoc>
    public async Task<User?> GetUserByMailAsync(string email) => await _context.Users.Include(u => u.Service)
                                                                                     .Include(u => u.Site)
                                                                                     .FirstOrDefaultAsync(u => u.Email == email);

    // </inheritedoc>
    public async Task<IEnumerable<User?>> GetUsersByNameAsync(string username) => await _context.Users.Include(u => u.Service)
                                                                                                      .Include(u => u.Site)
                                                                                                      .Where(u => u.UserName.Contains(username))
                                                                                                      .ToListAsync();

    // </inheritedoc>
    public async Task<IEnumerable<User?>> GetUsersBySiteAsync(Site site) => await _context.Users.Include(u => u.Service)
                                                                                                .Include(u => u.Site)
                                                                                                .Where(u => u.Site == site)
                                                                                                .ToListAsync();

    // </inheritedoc>
    public async Task<IEnumerable<User?>> GetUsersByServiceAsync(Service service) => await _context.Users.Include(u => u.Service)
                                                                                                         .Include(u => u.Site)
                                                                                                         .Where(u => u.Service == service)
                                                                                                         .ToListAsync();
    // </inheritedoc>

    public async Task<IEnumerable<User?>> GetUsersAsync() => await _context.Users.Include(u => u.Service)
                                                                                 .Include(u => u.Site).Select(u => new User()
                                                                                 {
                                                                                     Id = u.Id,
                                                                                     FirstName = u.FirstName,
                                                                                     LastName = u.LastName,
                                                                                     Email = u.Email,
                                                                                     Phone = u.Phone,
                                                                                     Gender = u.Gender,
                                                                                     MobilePhone = u.MobilePhone,
                                                                                     UserName = u.UserName,
                                                                                     UserType = u.UserType,
                                                                                     Site = u.Site,
                                                                                     Service = u.Service,
                                                                                     CreatedAt = u.CreatedAt,
                                                                                     UpdatedAt = u.UpdatedAt,
                                                                                 })
                                                                                 .ToListAsync();

    // </inheritedoc>
    public async Task<User?> PostUserAsync(User model)
    {
        if (model.Site?.Id != null)
        {
            Site? site = await _siteService.GetSiteAsync(model.Site.Id);
            if (site != null)
            {
                model.Site = site;
            }
        }

        if (model.Service?.Id != null)
        {
            Service? service = await _serviceService.GetServiceAsync(model.Service.Id);
            if (service != null)
            {
                model.Service = service;
            }
        }

        model.Password = model.Password?.Trim();
        model.FirstName = model.FirstName.Trim();
        model.LastName = model.LastName.Trim();
        model.Email = model.Email.Trim();
        model.UserName = model.UserName.Trim();
        model.Gender = model.Gender.Trim();
        model.MobilePhone = model.MobilePhone.Trim();
        model.Phone = model.Phone.Trim();

        model.Password = PasswordHelper.HashPassword(model.Password);

        User? newUser = (await _context.Users.AddAsync(model)).Entity;
        await _context.SaveChangesAsync();
        return newUser;
    }

    // </inheritedoc>
    public async Task<User?> PutUserAsync(User model)
    {

        if (model.Site?.Id != null)
        {
            Site? site = await _siteService.GetSiteAsNoTrackingAsync(model.Site.Id);
            if (site != null)
            {
                model.Site = site;
            }
        }

        if (model.Service?.Id != null)
        {
            Service? service = await _serviceService.GetServiceAsNoTrackingAsync(model.Service.Id);
            if (service != null)
            {
                model.Service = service;
            }
        }

        model.Password = model.Password?.Trim();
        model.FirstName = model.FirstName.Trim();
        model.LastName = model.LastName.Trim();
        model.Email = model.Email.Trim();
        model.UserName = model.UserName.Trim();
        model.Gender = model.Gender.Trim();
        model.MobilePhone = model.MobilePhone.Trim();
        model.Phone = model.Phone.Trim();

        User? dbUser = await GetUserAsync(model.Id);
        model.CreatedAt = dbUser?.CreatedAt;
        model.Password = model.Password != null ? PasswordHelper.HashPassword(model.Password) : dbUser?.Password;
        model.UpdatedAt = DateTime.Now;

        _context.Entry(model).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return model;
    }

    // </inheritedoc>
    public async Task<bool> DeleteUserAsync(int id)
    {
        User? dbUser = await GetUserAsync(id);
        if (dbUser != null)
        {
            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

    // </inheritedoc>
    public async Task<bool> UserExistsAsync(int id)
    {
        User? user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        if (user != null)
        {
            return true;
        }
        return false;
    }


}
