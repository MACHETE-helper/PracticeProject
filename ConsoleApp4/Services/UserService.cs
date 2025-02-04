using System.ComponentModel.DataAnnotations;
using Azure;
using ConsoleApp4.Dto;
using ConsoleApp4.Entities;
using ConsoleApp4.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp4.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task DeleteUserAsync(int id)
    {
        await _userRepository.DeleteUserAsync(id);
    }
    public async Task AddUserAsync(User user)
    {
        await _userRepository.AddUserAsync(user);
    }
    
    public async Task<User> GetUserByIdAsync(int userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user != null)
        {
            return user;
        }

        Console.WriteLine("User not found");
        return null;

    }

    public async Task<List<UserFullInfoDto>> GetFullInfoUsersAsync()
    {
        var users = await _userRepository.GetFullInfoUsersAsync();

        return users;
    }

    public async Task AddCompanyAsync(CompanyDto company)
    {
        await _userRepository.AddCompanyAsync(company);
    }

    public async Task AddUserToCompanyAsync(int userId, int companyId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
    
        if (user == null)
        {
            throw new ValidationException("User not found");
        }
        
        var company = await _userRepository.GetCompanyByIdAsync(companyId);
        if (company == null)
        {
            throw new ValidationException("Company not found");
        }
        
        user.CompanyId = company.Id;
        
        
        /*try
        {
            
        }
        catch (ValidationException ex)
        {
            Response.Code = 400;
        }
        catch (Exception ex)
        {
            Response.Code = 500;
        }*/
    }

    public async Task<CompanyDto?> GetCompanyByIdAsync(int companyId)
    {
        var company = await _userRepository.GetCompanyByIdAsync(companyId);

        if (company != null)
        {
            return company;
        }
        
        
        return null;
        
    }
}