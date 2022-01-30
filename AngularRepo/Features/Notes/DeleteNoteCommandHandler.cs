using AngularRepo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AngularRepo.Features.Notes;

public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand>
{
    private readonly ApplicationContext _context;

    public DeleteNoteCommandHandler(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public async Task<Unit> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
    {
        var note = await _context.Notes.FirstAsync(n => n.Id == command.Id, cancellationToken);
        _context.Remove(note);
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
