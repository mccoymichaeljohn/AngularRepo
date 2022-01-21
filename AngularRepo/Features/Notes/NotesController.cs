using AngularRepo.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularRepo.Features.Notes;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly ApplicationContext _context;

    public NotesController(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    [HttpGet]
    public async Task<IEnumerable<NotesListViewModel>> GetNotes()
    {
        var notes = await _context.Notes.ToArrayAsync();
        return notes.Select(n => new NotesListViewModel() 
        { 
            DateCreated = n.DateCreated, 
            DateDue = n.DateDue, 
            Id = n.Id, 
            Text = n.Text
        });
    }
}
