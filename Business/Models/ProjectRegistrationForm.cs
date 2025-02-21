namespace Business.Models;

public class ProjectRegistrationForm
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int? CustomerId { get; set; }
    public int? ProjectId { get; set; }
}
