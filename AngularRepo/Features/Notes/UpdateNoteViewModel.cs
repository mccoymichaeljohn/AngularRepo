namespace AngularRepo.Features.Notes;

public class UpdateNoteViewModel
{
    public string Text { get; set; }
    public DateTime? DateDue { get; set; }
    public bool IsCompleted { get; set; }
}