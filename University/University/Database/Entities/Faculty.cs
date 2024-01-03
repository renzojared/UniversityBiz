namespace University.Database.Entities;

public class Faculty : Inherent, IInherent
{
    public int Id { get; set; }
    public bool State { get; set; } = true;
    public HashSet<Career> Careers { get; set; } = new();
}

