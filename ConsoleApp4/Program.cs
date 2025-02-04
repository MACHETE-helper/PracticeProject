using ConsoleApp4;
using ConsoleApp4.Dto;
using ConsoleApp4.Entities;
using ConsoleApp4.Interfaces;
using ConsoleApp4.Repositories;
using ConsoleApp4.Services;
using Microsoft.EntityFrameworkCore;
using static ConsoleApp4.ApplicationContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//using var db = new ApplicationContext();
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer("DefaultConnection"));
        services.AddScoped<UserService>();
        services.AddScoped<IUserRepository, UserRepository>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

    bool isAvailable = db.Database.CanConnect();
    if (isAvailable)
        Console.WriteLine("Database is available");
    else
        Console.WriteLine("Database is not available");

//var tom = new User {Name = "Tom", Age = 33, Position = "Cashier"};
//var jack = new User{Name = "Jack", Age = 25, Position = "Cashier"};
//db.Users.Add(tom);
//db.Users.Add(jack);
//db.SaveChanges();
//db.Entry(tom).State = EntityState.Detached
// var company1 = new Company{Name = "Microsoft", Address = "USA"};
// var company2 = new Company{Name = "Yandex", Address = "Russia"};
// db.Companies.Add(company1);
// db.Companies.Add(company2);
// db.SaveChanges();
    db.Database.Migrate();

/*var userUpdate = db.Users.Where(u => u.Name == "Jack").ToList();

foreach (var u in userUpdate)
    u.CompanyId = 2;
db.SaveChanges();



var users = db.Users.ToList();

 var users = (from user in db.Users.Include(p => p.Company)
     select user).ToList();

var users = db.Users.Include(p => p.Company);

var users = db.Users.Where(p => p.Company!.Name == "Microsoft");*/

    var users1 = db.Users.Where(p => p.Id < 10).ToList();

    /*var users = db.Users.Select(u => new UserFullInfoDto()
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
    }).OrderBy(p => p.Age);*/

//var result = db.Users.Count(u=> u.CompanyId == 1);
//Console.WriteLine(result);

    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    var users = await userService.GetFullInfoUsersAsync(); //получаем всех пользователей
    var user = await userService.GetUserByIdAsync(3); //получаем пользователя по id
    if (user != null)
    {
        Console.WriteLine($"Id:{user.Id} Name:{user.Name} Company:{user.Company?.Name}");
    }

    // var testUser = new User() //добавляем нового пользователя
    // {
    //     Name = "Ash", Age = 29, Position = "Manager", CompanyId = 2
    // };
    // await userService.AddUserAsync(testUser);
    
    // var testCompany = new Company //добавляем новую компанию
    // {
    //    Name = "Google", Address = "UK"
    // };
    // await userService.AddCompanyAsync(testCompany);

    await userService.DeleteUserAsync(4); //удаляем пользователя если таковой существует

    var companyCheck = await userService.GetCompanyByIdAsync(100); //получаем компанию по id
    if (companyCheck != null)
    {
        Console.WriteLine($"Id:{companyCheck.Id} Name:{companyCheck.Name} Address:{companyCheck.Address}");
    }

    Console.WriteLine("Список объектов:");
    foreach (var u in users)
    {
        Console.WriteLine($"{u.Id}.{u.Name}({u.Position}) - {u.Age} works in {u.Company.ToString()}");
    }

    

}