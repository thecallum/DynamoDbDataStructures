using Amazon.DynamoDBv2.DataModel;
using TableWithSecondaryIndexes.Domain;
using TableWithSecondaryIndexes.Factories;
using TableWithSecondaryIndexes.UseCase;
using TableWithSecondaryIndexes.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDbDataStructures.Apps
{
    public class TableWithSecondaryIndexesApp
    {

        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IGetNoteByIdUseCase _getNotesByIdUseCase;
        private readonly ICreateNoteUseCase _createNoteUseCase;
        private readonly IGetAllNotesUseCase _getAllNotesUseCase;
        private readonly IDeleteNoteUseCase _deleteNoteUseCase;
        private readonly IUpdateNoteUseCase _updateNoteUseCase;

        public TableWithSecondaryIndexesApp(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;

            _getNotesByIdUseCase = new GetNoteByIdUseCase(_dynamoDbContext);
            _createNoteUseCase = new CreateNoteUseCase(_dynamoDbContext);
            _getAllNotesUseCase = new GetAllNotesUseCase(_dynamoDbContext);
            _deleteNoteUseCase = new DeleteNoteUseCase(_dynamoDbContext);
            _updateNoteUseCase = new UpdateNoteUseCase(_dynamoDbContext);
        }

        public async Task GetNote(Guid noteId, Guid accountId)
        {
            Console.WriteLine($"Getting note: {noteId}");

            var note = await _getNotesByIdUseCase.Execute(noteId, accountId);

            if (note == null)
            {
                Console.WriteLine($"Note With Id {noteId} could not be found");
                return;
            }

            Console.WriteLine(note.ToConsole(noteId));
        }

        public async Task<Guid> CreateNote(Note newNote, Guid accountId)
        {
            Console.WriteLine($"Creating note");

            var noteId = await _createNoteUseCase.Execute(newNote, accountId);

            Console.WriteLine($"Note Created with Id {noteId}");

            return noteId;
        }

        public async Task GetAllNotes(Guid accountId)
        {
            Console.WriteLine($"Getting all notes");

            var notes = await _getAllNotesUseCase.Execute(accountId);

            if (notes.Count() == 0)
            {
                Console.WriteLine("No Notes Could be found");
                return;
            }

            Console.WriteLine($"{notes.Count()} note(s) found");

            foreach (var note in notes)
            {
                Console.WriteLine(note.ToConsole(null));
            }
        }

        public async Task DeleteNote(Guid noteId, Guid accountId)
        {
            var note = await _deleteNoteUseCase.Execute(noteId, accountId);

            if (note == null)
            {
                Console.WriteLine($"Note with Id {noteId} could not be deleted because it doesnt exist");
                return;
            }

            Console.WriteLine("Note deleted");
        }

        public async Task UpdateNote(Guid noteId, Guid accountId, Note newNote)
        {
            var note = await _updateNoteUseCase.Execute(noteId, accountId, newNote);

            if (note == null)
            {
                Console.WriteLine($"Note with Id {noteId} could not be  updated because it doesnt exist");
                return;
            }

            Console.WriteLine("Note Updated");

            Console.WriteLine(newNote.ToConsole(noteId));
        }

    }
}

