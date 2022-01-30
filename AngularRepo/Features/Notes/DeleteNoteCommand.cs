using MediatR;

namespace AngularRepo.Features.Notes;

public class DeleteNoteCommand :  IRequest
{
    public DeleteNoteCommand(Guid id) => Id = id;
    public Guid Id { get; }
}
