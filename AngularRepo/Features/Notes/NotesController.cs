using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AngularRepo.Features.Notes;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly IMediator _mediatr;

    public NotesController(IMediator mediator)
    {
        _mediatr = mediator ?? throw new ArgumentNullException(nameof(_mediatr));
    }

    [HttpGet]
    public async Task<IEnumerable<NotesListViewModel>> GetNotes(CancellationToken cancellationToken = default)
    {
        return await _mediatr.Send(new GetNotesQuery(), cancellationToken);
    }

    [HttpPost]
    public async Task<NotesListViewModel> AddNote(AddNoteViewModel model, CancellationToken cancellationToken = default)
    {
        return await _mediatr.Send(new AddNoteCommand(model.Text, model.DateDue, model.NoteType), cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(Guid id, CancellationToken cancellationToken = default)
    {
        await _mediatr.Send(new DeleteNoteCommand(id), cancellationToken);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<NotesListViewModel> UpdateNote(Guid id, UpdateNoteViewModel model, CancellationToken cancellationToken = default)
    {
        return await _mediatr.Send(new UpdateNoteCommand(id, model.Text, model.DateDue, model.IsCompleted), cancellationToken);
    }
}
