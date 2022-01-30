using NUnit.Framework;
using System;
using System.Threading.Tasks;
using AngularRepo.Domain;
using AngularRepo.Features.Notes;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitTests.Features.Notes;

[TestFixture]
public class AddNoteCommandHandlerShould
{
    private DbContextOptions<ApplicationContext> _options;

    [SetUp]
    public void Setup()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        _options = new DbContextOptionsBuilder<ApplicationContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new ApplicationContext(_options);
        context.Database.EnsureCreated();
    }

    [Test]
    public async Task AddNote()
    {
        using (var context = new ApplicationContext(_options))
        {
            var notes = await context.Notes.ToArrayAsync();
            Assert.IsEmpty(notes);
            await new AddNoteCommandHandler(context).Handle(new AddNoteCommand("Sample", DateTime.Parse("2022-01-29"), NoteType.Task), default);
            notes = await context.Notes.ToArrayAsync();
            Assert.IsNotEmpty(notes);
            Assert.AreEqual(notes.Length, 1);
            Assert.AreEqual(notes.First().Text, "Sample");
            Assert.AreEqual(notes.First().DateDue, DateTime.Parse("2022-01-29"));
            Assert.AreEqual(notes.First().IsCompleted, false);
            Assert.AreEqual(notes.First().NoteType, NoteType.Task);
        }
    }

    [Test]
    public async Task AddMoreNotes()
    {
        using (var context = new ApplicationContext(_options))
        {
            var notes = await context.Notes.ToArrayAsync();
            Assert.IsEmpty(notes);
            context.Notes.Add(new Note(NoteType.Note, "text1", null));
            context.SaveChanges();
            await new AddNoteCommandHandler(context).Handle(new AddNoteCommand("Sample", DateTime.Parse("2022-01-29"), NoteType.Task), default);
            notes = await context.Notes.ToArrayAsync();
            Assert.IsNotEmpty(notes);
            Assert.AreEqual(notes.Length, 2);
        }
    }
}
