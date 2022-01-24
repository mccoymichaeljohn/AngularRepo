using System.ComponentModel.DataAnnotations;
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

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime? DateDue { get; private set; }

    public void SetText(string text) => Text = text;
    public void SetDateDue(DateTime? dateDue) => DateDue = dateDue;
}