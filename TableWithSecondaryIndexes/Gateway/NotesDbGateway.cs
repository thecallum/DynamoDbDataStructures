using Amazon.DynamoDBv2.DataModel;
using TableWithSecondaryIndexes.Domain;
using TableWithSecondaryIndexes.Factories;
using TableWithSecondaryIndexes.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;

namespace TableWithSecondaryIndexes.Gateway
{
    public class NotesDbGateway : INotesDbGateway
    {
        private readonly IDynamoDBContext _dynamoDbContext;
       // private readonly AmazonDynamoDBClient _client;

        public NotesDbGateway(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;
            //_client = client;
        }

        public async Task<Guid> CreateNote(Note newNote, Guid accountId)
        {
            var noteDb = newNote.ToDatabase(null, accountId);

            await _dynamoDbContext.SaveAsync(noteDb);
            return noteDb.NoteId;
        }

        public async Task<Note> DeleteNote(Guid noteId, Guid accountId)
        {
            var note = await LoadNote(noteId, accountId);
            if (note == null) return null;

            await _dynamoDbContext.DeleteAsync<NoteDb>(accountId, noteId);

            // if note was found
            return note.ToDomain();
        }

        public async Task<IEnumerable<Note>> GetAllNotes(Guid accountId)
        {
            var notes = new List<NoteDb>();

            var search = _dynamoDbContext.QueryAsync<NoteDb>(accountId, new DynamoDBOperationConfig
            {
                IndexName = "CreatedIndex"
            });

            do
            {
                var newNotes = await search.GetNextSetAsync();
                notes.AddRange(newNotes);

            } while (search.IsDone == false);

            return notes.Select(x => x.ToDomain());
        }

        public async Task<Note> GetNoteById(Guid noteId, Guid accountId)
        {

            var note = await LoadNote(noteId, accountId);
            if (note == null) return null;

            return note.ToDomain();

        }

        public async Task<Note> UpdateNote(Guid noteId, Guid accountId, Note newNote)
        {
            var existingNote = await LoadNote(noteId, accountId);
            if (existingNote == null) return null;

            await _dynamoDbContext.SaveAsync(newNote.ToDatabase(noteId, accountId));

            return newNote;
            
        }

        private async Task<NoteDb> LoadNote(Guid noteId, Guid accountId)
        { 
            return await _dynamoDbContext.LoadAsync<NoteDb>(accountId, noteId).ConfigureAwait(false);
        }
    }
}
