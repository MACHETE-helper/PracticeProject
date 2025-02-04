using ConsoleApp4.Entities;

namespace ConsoleApp4.Dto;

public class UserFullInfoDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Position { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public int? CompanyId { get; set; }
    public CompanyDto? Company { get; set; }
    
}