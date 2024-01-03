namespace University.Database.Entities;

public class Career : Inherent, IInherent
{
    public int Id { get; set; }
    public bool State { get; set; } = true;
    public int FacultyId { get; set; }
    public Faculty Faculty { get; set; }
}

