namespace ConsoleApp4.Entities;


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