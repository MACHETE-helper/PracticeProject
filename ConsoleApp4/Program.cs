using ConsoleApp4;
using Microsoft.EntityFrameworkCore;
using static ConsoleApp4.ApplicationContext;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

//using var db = new ApplicationContext();
var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        // Регистрация ApplicationContext с подключением к базе данных
        services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer("DefaultConnection")); // Укажите строку подключения
    })
    .Build();

// Получение сервиса через ServiceProvider
using var scope = host.Services.CreateScope();
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
// db.Database.Migrate();

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

var users = db.Users.Select(u => new
{
    Id = u.Id,
    Name = u.Name,
    Position = u.Position,
    Age = u.Age,
    Company = u.Company.Name
}).OrderBy(p => p.Age);

//var result = db.Users.Count(u=> u.CompanyId == 1);
//Console.WriteLine(result);


Console.WriteLine("Список объектов:");
foreach (var u in users)
{
    Console.WriteLine($"{u.Id}.{u.Name}({u.Position}) - {u.Age} works in {u.Company}");
}


public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Position { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public Company? Company { get; set; }
    
}

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Address { get; set; }
    public List<User> Users { get; set; } = new();
}