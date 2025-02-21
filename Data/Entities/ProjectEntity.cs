using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity
{
    [Key]
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }

    public int CustomerId { get; set; }
    public CustomerEntity Customer { get; set; } = null!;

}