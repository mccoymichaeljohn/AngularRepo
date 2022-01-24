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
        var notes = await _context.Notes.OrderBy(n => n.DateCreated).ToArrayAsync();
        return notes.Select(n => new NotesListViewModel() 
        { 
            DateCreated = n.DateCreated, 
            DateDue = n.DateDue, 
            Id = n.Id, 
            Text = n.Text
        });
    }

    [HttpPost]
    public async Task<NotesListViewModel> AddNote(AddNoteViewModel model)
    {
        var note = new Note(model.Text, model.DateDue);
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();
        return new NotesListViewModel()
        {
            DateCreated = note.DateCreated,
            DateDue = note.DateDue,
            Id = note.Id,
            Text = note.Text
        };
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNote(Guid id)
    {
        var note = await _context.Notes.FirstAsync(n => n.Id == id);
        _context.Remove(note);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<NotesListViewModel> UpdateNote(Guid id, UpdateNoteViewModel model)
    {
        var note = await _context.Notes.FirstAsync(n => n.Id == id);
        note.SetText(model.Text);
        note.SetDateDue(model.DateDue);
        await _context.SaveChangesAsync();
        return new NotesListViewModel()
        {
            DateCreated = note.DateCreated,
            DateDue = note.DateDue,
            Id = note.Id,
            Text = note.Text
        };
    }
}
