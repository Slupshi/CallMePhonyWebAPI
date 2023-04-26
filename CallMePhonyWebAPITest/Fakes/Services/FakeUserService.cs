using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;

namespace CallMePhonyWebAPITest.Fakes.Services
{
    public class UserServiceFake : IUserService
    {
        private readonly List<User> _users;

        public UserServiceFake() 
        {
            _users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    FirstName = "Robert",
                    LastName = "DACOSTA",
                    UserName = "DACOSTA Robert",
                    UserType = CallMePhonyWebAPI.Models.Enums.UserType.Visitor,
                    Phone = "0123456789",
                    MobilePhone = "0987654321",
                    Email = "robert.dacosta@gmail.com",
                    Gender = "HOMME",
                    Site = new Site()
                    {
                        Id = 1,
                    },
                    Service = new Service()
                    {
                        Id = 1,
                    }
                },
                new User()
                {
                    Id = 2,
                    FirstName = "Robert",
                    LastName = "DACOSTA",
                    UserName = "DACOSTA Robert",
                    UserType = CallMePhonyWebAPI.Models.Enums.UserType.Visitor,
                    Phone = "0123456789",
                    MobilePhone = "0987654321",
                    Email = "robert.dacosta@gmail.com",
                    Gender = "HOMME",
                    Site = new Site()
                    {
                        Id = 1,
                    },
                    Service = new Service()
                    {
                        Id = 1,
                    }
                },
                new User()
                {
                    Id = 3,
                    FirstName = "Robert",
                    LastName = "DACOSTA",
                    UserName = "DACOSTA Robert",
                    UserType = CallMePhonyWebAPI.Models.Enums.UserType.Visitor,
                    Phone = "0123456789",
                    MobilePhone = "0987654321",
                    Email = "robert.dacosta@gmail.com",
                    Gender = "HOMME",
                    Site = new Site()
                    {
                        Id = 1,
                    },
                    Service = new Service()
                    {
                        Id = 1,
                    }
                },
            };
        }

        public async Task<User?> GetUserAsync(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User?> GetUserByMailAsync(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public async Task<IEnumerable<User?>> GetUsersByNameAsync(string username)
        {

            return _users.Where(u => u.UserName.Contains(username));
        }

        public async Task<IEnumerable<User?>> GetUsersByServiceAsync(Service service)
        {
            return _users.Where(u => u.Service == service);
        }

        public async Task<IEnumerable<User?>> GetUsersBySiteAsync(Site site)
        {
            return _users.Where(u => u.Site == site);
        }

        public async Task<IEnumerable<User?>> GetUsersAsync()
        {
            return _users;
        }

        public async Task<User?> PostUserAsync(User model)
        {
            _users.Add(model);
            return model;
        }

        public async Task<User?> PutUserAsync(User model)
        {
            var user = await GetUserAsync(model.Id);
            var index = _users.IndexOf(user);
            _users[index] = model;
            return model;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserAsync(id);
            _users.Remove(user);
            return true;
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            return _users.Any(u => u.Id == id);
        }
    }
}
