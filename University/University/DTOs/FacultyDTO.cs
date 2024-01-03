namespace University.DTOs;

public class FacultyDTO : InherentDTO
{
    public List<CareerDTO> Careers { get; set; } = new();
}

