using Amazon.DynamoDBv2.DataModel;
using SimpleTable.Domain;
using SimpleTable.Factories;
using SimpleTable.UseCase;
using SimpleTable.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamoDbDataStructures.Apps
{
    public class SimpleTableApp
    {
        private readonly IDynamoDBContext _dynamoDbContext;
        private readonly IGetNoteByIdUseCase _getNotesByIdUseCase;
        private readonly ICreateNoteUseCase _createNoteUseCase;
        private readonly IGetAllNotesUseCase _getAllNotesUseCase;
        private readonly IDeleteNoteUseCase _deleteNoteUseCase;
        private readonly IUpdateNoteUseCase _updateNoteUseCase;

        public SimpleTableApp(IDynamoDBContext dynamoDbContext)
        {
            _dynamoDbContext = dynamoDbContext;

            _getNotesByIdUseCase = new GetNoteByIdUseCase(_dynamoDbContext);
            _createNoteUseCase = new CreateNoteUseCase(_dynamoDbContext);
            _getAllNotesUseCase = new GetAllNotesUseCase(_dynamoDbContext);
            _deleteNoteUseCase = new DeleteNoteUseCase(_dynamoDbContext);
            _updateNoteUseCase = new UpdateNoteUseCase(_dynamoDbContext);
        }

        public async Task GetNote(Guid id)
        {
            Console.WriteLine($"Getting note: {id}");

            var note = await _getNotesByIdUseCase.Execute(id);

            if (note == null)
            {
                Console.WriteLine($"Note With Id {id} could not be found");
                return;
            }

            Console.WriteLine(note.ToConsole(id));
        }

        public async Task<Guid> CreateNote(Note newNote)
        {
            Console.WriteLine($"Creating note");

            var noteId = await _createNoteUseCase.Execute(newNote);

            Console.WriteLine($"Note Created with Id {noteId}");

            return noteId;
        }

        public async Task GetAllNotes()
        {
            Console.WriteLine($"Getting all notes");

            var notes = await _getAllNotesUseCase.Execute();

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

        public async Task DeleteNote(Guid id)
        {
            var note = await _deleteNoteUseCase.Execute(id);

            if (note == null)
            {
                Console.WriteLine($"Note with Id {id} could not be deleted because it doesnt exist");
                return;
            }

            Console.WriteLine("Note deleted");
        }

        public async Task UpdateNote(Guid id, Note newNote)
        {
            var note = await _updateNoteUseCase.Execute(id, newNote);

            if (note == null)
            {
                Console.WriteLine($"Note with Id {id} could not be  updated because it doesnt exist");
                return;
            }

            Console.WriteLine("Note Updated");

            Console.WriteLine(newNote.ToConsole(id));
        }

    }
}
