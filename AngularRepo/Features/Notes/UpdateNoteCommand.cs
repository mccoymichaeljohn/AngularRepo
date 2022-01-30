using MediatR;

namespace AngularRepo.Features.Notes;

public class UpdateNoteCommand : IRequest<NotesListViewModel>
{
    public UpdateNoteCommand(Guid id, string text, DateTime? dateDue, bool isCompleted)
    {
        Id = id;
        Text = text;
        DateDue = dateDue;
        IsCompleted = isCompleted;
    }

    public Guid Id { get; }
    public string Text { get; }
    public DateTime? DateDue { get; }
    public bool IsCompleted { get; }
}
