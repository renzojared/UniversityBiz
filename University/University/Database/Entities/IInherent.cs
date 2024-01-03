namespace University.Database.Entities;

public interface IInherent
{
    public int Id { get; set; }
    public bool State { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime? ModificationDate { get; set; }
}

