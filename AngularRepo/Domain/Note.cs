using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularRepo.Domain;

public class Note
{
    public Note(NoteType noteType, string text, DateTime? dateDue)
    {
        Id = Guid.NewGuid();
        DateCreated = DateTime.Now;
        NoteType = noteType;
        Text = text;
        DateDue = dateDue;
        IsCompleted = false;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; private set; }
    public DateTime DateCreated { get; private set; }
    public NoteType NoteType { get; private set; }
    public string Text { get; private set; }
    public DateTime? DateDue { get; private set; }
    public bool IsCompleted { get; private set; }

    public void SetText(string text) => Text = text;
    public void SetDateDue(DateTime? dateDue) => DateDue = dateDue;
    public void SetType(NoteType noteType) => NoteType = noteType;
    public void SetCompleted(bool isCompleted) => IsCompleted = isCompleted;
}