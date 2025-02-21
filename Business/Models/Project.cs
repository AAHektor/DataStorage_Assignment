namespace Business.Models;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? ProjectNumber { get; set; }
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public string UserName { get; set; } = null!;

}
