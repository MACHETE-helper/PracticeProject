using ConsoleApp4.Dto;
using ConsoleApp4.Entities;
using ConsoleApp4.Interfaces;
using ConsoleApp4.Services;
using NSubstitute;

namespace ConsoleApp4.Tests.UnitTests.UserRepositoryTests;

public class UserRepositoryTests
{
    private IUserRepository _userRepository;
    private UserService _userService;

    [SetUp]
    public void Setup()
    {
        _userRepository = Substitute.For<IUserRepository>();
        _userService = new UserService(_userRepository);
    }

    [Test]
    public async Task GetFullInfoUsersAsync_ShouldReturnUsers()
    {
        // Arrange
        var expectedUsers = new List<UserFullInfoDto>
        {
            new UserFullInfoDto {Id = 1, Name = "Tom", Position = "Cashier", Age = 30},
            new UserFullInfoDto {Id = 2, Name = "Anna", Position = "Manager", Age = 25}
        };
        _userRepository.GetFullInfoUsersAsync()
            .Returns(expectedUsers);
        
        // Act
        var users = await _userService.GetFullInfoUsersAsync();

        // Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Count, Is.EqualTo(expectedUsers.Count));
        Assert.That(users[0].Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetUserByIdAsync_ShouldReturnUser()
    {
        // Arrange
        var expectedUser = new User {Id = 2, Name = "Max", Position = "Cashier", Age = 25};
        _userRepository.GetUserByIdAsync(2).Returns(expectedUser);
        // Act
        var users = await _userService.GetUserByIdAsync(2);
        
        // Assert
        Assert.That(users, Is.Not.Null);
        Assert.That(users.Id, Is.EqualTo(2));
        Assert.That(users.Name, Is.EqualTo("Max"));
    }

    [Test]
    public async Task AddUserAsync_ShouldAddUser()
    {
        // Arrange
        var mockUser = new User()
        {
            Id = 1,
            Name = "Mocked User",
            Age = 23,
            Position = "Cashier"
        };
        
        // Act
        await _userService.AddUserAsync(mockUser);
        
        // Assert
        await _userRepository.Received(1).AddUserAsync(mockUser);
        Assert.That(mockUser.Id, Is.EqualTo(1));
    }

    [Test]
    public async Task DeleteUserAsync_ShouldDeleteUser()
    {
        //Arrange
        var currentUsers = new User {Id = 3, Name = "Max", Position = "Boss", Age = 30};
        
        _userRepository.GetUserByIdAsync(3).Returns(currentUsers);
        _userRepository.DeleteUserAsync(3).Returns(Task.CompletedTask);
        //Act
        await _userService.DeleteUserAsync(3);
        //Assert
        await _userRepository.Received(1).DeleteUserAsync(3);
        
        _userRepository.GetUserByIdAsync(3)!.Returns(Task.FromResult<User>(null!));
        var deletedUser = await _userService.GetUserByIdAsync(3);
        Assert.That(deletedUser, Is.Null);
        
    }

    [Test]
    public async Task GetCompanyByIdAsync_ShouldReturnCompany()
    {
        //Arrange
        var expectedCompanies = new CompanyDto {Id = 1, Name = "Yandex", Address = "Russia"};
        _userRepository.GetCompanyByIdAsync(1).Returns(expectedCompanies);
        //Act
        var company = await _userService.GetCompanyByIdAsync(1);
        //Assert
        Assert.That(company?.Name, Is.EqualTo("Yandex"));
    }
    
    [Test]
    public async Task AddCompanyAsync_ShouldAddCompany()
    {
        var mockCompany = new CompanyDto()
        {
            Id = 1,
            Name = "Yandex",
            Address = "123 Main Street",
        };
        
        await _userService.AddCompanyAsync(mockCompany);
        await _userRepository.Received(1).AddCompanyAsync(mockCompany);
        
    }
}