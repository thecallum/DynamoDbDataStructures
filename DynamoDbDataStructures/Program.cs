using AutoFixture;
using DynamoDbDataStructures.Apps;
using SimpleTable.Domain;
using SimpleTable.Infrastructure;
using System;
using System.Threading.Tasks;
using TableWithSecondaryIndexes.Infrastructure;
using TableWithSortKey.Infrastructure;
using App2Note = TableWithSortKey.Domain.Note;
using App3Note = TableWithSecondaryIndexes.Domain.Note;

namespace DynamoDbDataStructures
{
    class Program
    {
        private static readonly Fixture _fixture = new Fixture();

        static async Task Main(string[] args)
        {
            var database = new DatabaseConnection();

            //await RunSimpleTableApp(database);
            //await RunTableWithSortKeyApp(database);
            await RunTableWithSecondayIndexes(database);

            Console.ReadKey();
        }

        private static async Task RunTableWithSecondayIndexes(DatabaseConnection database)
        {
            var app = new TableWithSecondaryIndexesApp(database.Context);

            var setupDatabase = new SetupDatabaseTableWithSecondaryIndexes(database.Client, database.Context);
            await setupDatabase.SetupTables();

            var accountId = Guid.NewGuid();

            // create multiple notes with random date
            var randomNotes = _fixture.CreateMany<App3Note>(10);

            // insert notes into database
            foreach (var note in randomNotes)
            {
                await app.CreateNote(note, accountId);
            }

            // get all notes - they should be in date order
            app.GetAllNotes(accountId);
        }

        private static async Task RunTableWithSortKeyApp(DatabaseConnection database)
        {
            var app = new TableWithSortKeyApp(database.Context);

            var setupDatabase = new SetupDatabaseTableWithSortKey(database.Client, database.Context);
            await setupDatabase.SetupTables();

            var accountOneId = Guid.NewGuid();
            var accountTwoId = Guid.NewGuid();

            // create first note - account 1
            var firstNote = new App2Note
            {
                Title = "First Note",
                Contents = "First Note Contents",
                Created = DateTime.UtcNow
            };

            var firstNoteId = await app.CreateNote(firstNote, accountOneId);

            // get all notes - account 1
            await app.GetAllNotes(accountOneId);

            // get all notes - account 2 (no results)
            await app.GetAllNotes(accountTwoId);

            // create second note - account 1
            var secondNote = new App2Note
            {
                Title = "Second Note",
                Contents = "Second Note Contents",
                Created = DateTime.UtcNow
            };

            var secondNoteId = await app.CreateNote(secondNote, accountOneId);

            // update second note - account 1
            var secondNoteWithUpdates = new App2Note
            {
                Title = "Second Note With Updates",
                Contents = "Second Note Contents With Updates",
                Created = DateTime.UtcNow
            };

            await app.UpdateNote(secondNoteId, accountOneId, secondNoteWithUpdates);

            // get second note - account 1
            await app.GetNote(secondNoteId, accountOneId);

            // get all notes - account 1
            await app.GetAllNotes(accountOneId);

            // get all notes - account 2
            await app.GetAllNotes(accountTwoId);

            // delete note 1 - account 1
            await app.DeleteNote(firstNoteId, accountOneId);

            // delete note 1 - account 1 (doesnt exist)
            await app.DeleteNote(firstNoteId, accountOneId);

            // get all notes - account 1
            await app.GetAllNotes(accountOneId);


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
