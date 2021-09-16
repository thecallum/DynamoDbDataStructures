using DynamoDbDataStructures.Apps;
using SimpleTable.Domain;
using SimpleTable.Infrastructure;
using System;
using System.Threading.Tasks;

namespace DynamoDbDataStructures
{
    class Program
    {

        static async Task Main(string[] args)
        {
            var database = new DatabaseConnection();

            await RunSimpleTableApp(database);

            Console.ReadKey();
        }

        private static async Task RunSimpleTableApp(DatabaseConnection database)
        {
            var app = new SimpleTableApp(database.Context);

            var setupDatabase = new SetupDatabase(database.Client, database.Context);
            await setupDatabase.SetupTables();

            // get all notes, should be none.
            await app.GetAllNotes();

            // create note
            var firstNote = new Note
            {
                Title = "First Note",
                Contents = "First Note Contents",
                Created = DateTime.UtcNow
            };

            var firstNoteId = await app.CreateNote(firstNote);

            // get created note
            await app.GetNote(firstNoteId);

            // create another note
            var secondNote = new Note
            {
                Title = "Second Note",
                Contents = "Second Note Contents",
                Created = DateTime.UtcNow
            };

            var secondNoteId = await app.CreateNote(secondNote);

            // update other note
            var secondNoteWithUpdates = new Note
            {
                Title = "Second Note With Updates",
                Contents = "Second Note Contents With Updates",
                Created = DateTime.UtcNow
            };

            await app.UpdateNote(secondNoteId, secondNoteWithUpdates);

            // get other note
            await app.GetNote(secondNoteId);

            // get all notes
            await app.GetAllNotes();

            // delete first note
            await app.DeleteNote(firstNoteId);

            // delete first note again 
            await app.DeleteNote(firstNoteId);

            // get all notes again
            await app.GetAllNotes();
        }
    }
}
