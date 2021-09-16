using Amazon.DynamoDBv2.DataModel;
using SimpleTable.Domain;
using SimpleTable.Gateway;
using SimpleTable.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace SimpleTable.UseCase
{
    public class CreateNoteUseCase : ICreateNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public CreateNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Guid> Execute(Note newNote)
        {
            var response = await _notesGateway.CreateNote(newNote).ConfigureAwait(false);

            return response;
        }
    }
}
