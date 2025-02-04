using ConsoleApp4.Dto;
using ConsoleApp4.Entities;
using ConsoleApp4.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp4.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _dbContext;

    public UserRepository(ApplicationContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<UserFullInfoDto>> GetFullInfoUsersAsync()
    {
        var users = _dbContext.Users.Select(u => new UserFullInfoDto()
        {
            Id = u.Id,
            Name = u.Name,
            Position = u.Position,
            Age = u.Age,
            Email = u.Email,
            Phone = u.Phone,
            CompanyId = u.CompanyId,
            Company = u.Company != null
                ? new CompanyDto()
                {
                    Id = u.CompanyId.Value,
                    Name = u.Company.Name,
                    Address = u.Company.Address
                }
                : null
        });

        return await users.ToListAsync();
    }
    public async Task<User?> GetUserByIdAsync(int userId)
    {
        var user = await _dbContext.Users.Include(user => user.Company).FirstOrDefaultAsync(u => u.Id == userId);
        //return user;
        if (user == null)
        {
            return null;
        }
        return new User()
        {
            Id = user.Id,
            Name = user.Name,
            Position = user.Position,
            Age = user.Age,
            Email = user.Email,
            Phone = user.Phone,
            CompanyId = user.CompanyId,
            Company = user.Company != null
                ? new Company()
                {
                    Id = user.CompanyId.Value,
                    Name = user.Company.Name,
                    Address = user.Company.Address
                }
                : null
        };
    }
    public async Task AddUserAsync(User newUser)
    {
        var user = new User()
        {
            Id = newUser.Id,
            Name = newUser.Name,
            Position = newUser.Position,
            Age = newUser.Age,
            Email = newUser.Email,
            Phone = newUser.Phone,
            CompanyId = newUser.CompanyId

        };
        
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        
    }
    public async Task DeleteUserAsync(int userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null)
        {
            Console.WriteLine($"User {userId} not found");
        }
        else
        {
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task AddCompanyAsync(CompanyDto newCompany)
    {
        var company = new Company()
        {
            Id = newCompany.Id,
            Name = newCompany.Name,
            Address = newCompany.Address
        };
        
        await _dbContext.AddAsync(company);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<CompanyDto?> GetCompanyByIdAsync(int companyId)
    {
        var company = await _dbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);

        if (company == null)
        {
            Console.WriteLine($"Company {companyId} not found");
            return null;
        }
        return new CompanyDto(){
            Id = company.Id,
            Name = company.Name,
            Address = company.Address
        };
    }

    // public async Task AddUserToCompanyAsync(int userId, int companyId)
    // {
    //     var company = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == userId);
    //     if (GetCompanyByIdAsync(company) != null)
    //     {
    //         
    //     }
    //     
    //     company.CompanyId = companyId;
    // }
    
}