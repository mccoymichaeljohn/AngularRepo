using AngularRepo.Domain;
using MediatR;

namespace AngularRepo.Features.Notes;

public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, NotesListViewModel>
{
    private readonly ApplicationContext _context;

    public AddNoteCommandHandler(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<NotesListViewModel> Handle(AddNoteCommand command, CancellationToken cancellationToken)
    {
        var note = new Note(command.NoteType, command.Text, command.DateDue);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);
        return new NotesListViewModel()
        {
            DateCreated = note.DateCreated,
            DateDue = note.DateDue,
            Id = note.Id,
            Text = note.Text,
            NoteType = note.NoteType,
            IsCompleted = note.IsCompleted
        };
    }
}
