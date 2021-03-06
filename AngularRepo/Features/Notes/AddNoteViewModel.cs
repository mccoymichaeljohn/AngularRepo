using AngularRepo.Domain;

namespace AngularRepo.Features.Notes;

public class AddNoteViewModel
{
    public string Text { get; set; }
    public DateTime? DateDue { get; set; }
    public NoteType NoteType { get; set; }
}
