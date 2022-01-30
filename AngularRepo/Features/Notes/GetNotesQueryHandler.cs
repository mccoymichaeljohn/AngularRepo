using AngularRepo.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AngularRepo.Features.Notes;

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IEnumerable<NotesListViewModel>>
{
    private readonly ApplicationContext _context;

    public GetNotesQueryHandler(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<NotesListViewModel>> Handle(GetNotesQuery query, CancellationToken cancellationToken)
    {
        var notes = await _context.Notes.OrderBy(n => n.DateCreated).ToArrayAsync(cancellationToken);
        return notes.Select(n => new NotesListViewModel()
        {
            DateCreated = n.DateCreated,
            DateDue = n.DateDue,
            Id = n.Id,
            Text = n.Text,
            IsCompleted = n.IsCompleted,
            NoteType = n.NoteType
        });
    }
}
