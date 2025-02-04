using ConsoleApp4.Dto;
using ConsoleApp4.Entities;

namespace ConsoleApp4.Interfaces;

public interface IUserRepository
{
    Task<List<UserFullInfoDto>> GetFullInfoUsersAsync();
    Task AddUserAsync(User user);
    Task AddCompanyAsync(CompanyDto company);
    Task DeleteUserAsync(int userId);
    Task<User?> GetUserByIdAsync(int userId);
    Task<CompanyDto?> GetCompanyByIdAsync(int companyId);
}