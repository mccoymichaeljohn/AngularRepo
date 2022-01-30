using AngularRepo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AngularRepo.Features.Notes;

public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NotesListViewModel>
{
    private readonly ApplicationContext _context;

    public UpdateNoteCommandHandler(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<NotesListViewModel> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await _context.Notes.FirstAsync(n => n.Id == command.Id, cancellationToken);
        note.SetText(command.Text);
        note.SetDateDue(command.DateDue);
        note.SetCompleted(command.IsCompleted);
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
