using MediatR;

namespace AngularRepo.Features.Notes;

public class GetNotesQuery : IRequest<IEnumerable<NotesListViewModel>>
{
}
