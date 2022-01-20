using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularRepo.Features.Notes;
[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    [HttpGet]
    public IEnumerable<NotesListViewModel> GetNotes()
    {
        return new List<NotesListViewModel>();
    }
}
