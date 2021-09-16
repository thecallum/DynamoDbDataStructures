using Amazon.DynamoDBv2.DataModel;
using SimpleTable.Domain;
using SimpleTable.Factories;
using SimpleTable.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTable.Gateway
{
    public class NotesDbGateway : INotesDbGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;

        public NotesDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
        }

        public async Task<Guid> CreateNote(Note newNote)
        {
            var noteDb = newNote.ToDatabase();
            await _dynamoDbContext.SaveAsync(noteDb);

            return (Guid)noteDb.Id;
        }

        public async Task<Note> DeleteNote(Guid id)
        {
            var note = await LoadNote(id);
            if (note == null) return null;

            await _dynamoDbContext.DeleteAsync<NoteDb>(id);

            // if note was found
            return note.ToDomain();
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            var conditions = new List<ScanCondition>();

            var notes = await _dynamoDbContext.ScanAsync<NoteDb>(conditions).GetRemainingAsync();
            if (notes.Count == 0) return new List<Note>();

            return notes.Select(x => x.ToDomain()).ToList();
        }

        public async Task<Note> GetNoteById(Guid id)
        {
            var note = await LoadNote(id);
            if (note == null) return null;

            return note.ToDomain();
        }

        public async Task<Note> UpdateNote(Guid id, Note newNote)
        {
            var existingNote = await LoadNote(id);
            if (existingNote == null) return null;

            await _dynamoDbContext.SaveAsync(newNote.ToDatabase(id));

            return newNote;
        }

        private async Task<NoteDb> LoadNote(Guid id)
        {
            return await _dynamoDbContext.LoadAsync<NoteDb>(id).ConfigureAwait(false);
        }
    }
}
