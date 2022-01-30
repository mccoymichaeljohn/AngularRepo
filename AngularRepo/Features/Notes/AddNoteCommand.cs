using AngularRepo.Domain;
using MediatR;

namespace AngularRepo.Features.Notes;

public class AddNoteCommand : IRequest<NotesListViewModel>
{
    public AddNoteCommand(string text, DateTime? dateDue, NoteType noteType)
    {
        Text = text;
        DateDue = dateDue;
        NoteType = noteType;
    }

    public string Text { get; }
    public DateTime? DateDue { get; }
    public NoteType NoteType { get; }
}
