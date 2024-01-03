using System.ComponentModel.DataAnnotations;

namespace University.DTOs;

public class CareerCreationDTO
{
    [Required]
    public int FacultyId { get; set; }
    [Required(ErrorMessage = "Value is required")]
    [StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Value is required")]
    [StringLength(5)]
    public string Code { get; set; }
}

