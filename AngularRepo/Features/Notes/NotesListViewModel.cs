﻿namespace AngularRepo.Features.Notes;

public class NotesListViewModel
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateDue { get; set; }
}