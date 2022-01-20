using System.ComponentModel.DataAnnotations.Schema;

namespace AngularRepo.Domain;

public class Note
{
    public Note(string text, DateTime? dateDue)
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.Now;
        Text = text;
        DateDue = dateDue;
    }
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; }
    public string Text { get; private set; }
    public DateTime DateCreated { get; }
    public DateTime? DateDue { get; private set; }
}