using AngularRepo.Domain;
using AngularRepo.Features.Notes;
using MediatR;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace UnitTests.Features.Notes;

[TestFixture]
public class NotesControllerShould
{
    private readonly Mock<IMediator> _mediator = new();
    private NotesController _controller;

    [SetUp]
    public void Setup()
    {
        _controller = new NotesController(_mediator.Object);
    }

    [Test]
    public async Task SendGetNotesQuery()
    {
        Assert.DoesNotThrowAsync(() => _controller.GetNotes());
        await _controller.GetNotes();
        _mediator.Verify(m => m.Send(It.IsAny<GetNotesQuery>(), default));
    }

    [Test]
    public async Task SendAddNoteCommand()
    {
        var model = new AddNoteViewModel()
        {
            DateDue = DateTime.Parse("2022-01-29"),
            Text = "SampleText",
            NoteType = NoteType.Note
        };
        Assert.DoesNotThrowAsync(() => _controller.AddNote(model), default);
        await _controller.AddNote(model, default);
        _mediator.Verify(m => m.Send(It.IsAny<AddNoteCommand>(), default));
        _mediator.Verify(m => m.Send(It.Is<AddNoteCommand>(c => 
            c.Text == "SampleText" && 
            c.DateDue == DateTime.Parse("2022-01-29") &&
            c.NoteType == NoteType.Note),
            default));
    }

    [Test]
    public async Task SendDeleteNoteCommand()
    {
        var id = Guid.NewGuid();
        Assert.DoesNotThrowAsync(() => _controller.DeleteNote(id));
        await _controller.DeleteNote(id, default);
        _mediator.Verify(m => m.Send(It.IsAny<DeleteNoteCommand>(), default));
        _mediator.Verify(m => m.Send(It.Is<DeleteNoteCommand>(c => c.Id == id), default));
    }

    [Test]
    public async Task SendUpdateNoteCommand()
    {
        var id = Guid.NewGuid();
        var model = new UpdateNoteViewModel()
        {
            DateDue = DateTime.Parse("2022-01-29"),
            Text = "Sample",
            IsCompleted = false
        };
        Assert.DoesNotThrowAsync(() => _controller.UpdateNote(id, model));
        await _controller.UpdateNote(id, model, default);
        _mediator.Verify(m => m.Send(It.IsAny<UpdateNoteCommand>(), default)); 
        _mediator.Verify(m => m.Send(It.Is<UpdateNoteCommand>(c =>
             c.Id == id &&
             c.Text == "Sample" &&
             c.DateDue == DateTime.Parse("2022-01-29") &&
             c.IsCompleted == false),
             default));
    }
}
