using CallMePhonyWebAPI.Controllers;
using CallMePhonyWebAPI.DTO.Responses;
using CallMePhonyWebAPI.Models;
using CallMePhonyWebAPI.Services;
using CallMePhonyWebAPITest.Fakes.Services;
using Microsoft.AspNetCore.Mvc;

namespace CallMePhonyWebAPITest
{
    public class UserControllerTest
    {
        private readonly IUserService _userService;
        private readonly UsersController _usersController;

        public UserControllerTest()
        {
            _userService = new UserServiceFake();
            _usersController = new UsersController(_userService);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = await _usersController.GetUsersAsync();
            // Assert
            var result = okResult.Result as OkObjectResult;            
            var items = Assert.IsType<List<User>>((result.Value as UsersResponse).Users);
            Assert.Equal(3, items.Count);
        }
    }
}