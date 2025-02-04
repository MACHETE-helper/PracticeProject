namespace ConsoleApp4.Entities;

public class Company
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Address { get; set; }
    public List<User> Users { get; set; } = new();
}






