using Amazon.DynamoDBv2.DataModel;
using TableWithSortKey.Domain;
using TableWithSortKey.Gateway;
using TableWithSortKey.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace TableWithSortKey.UseCase
{
    public class CreateNoteUseCase : ICreateNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public CreateNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Guid> Execute(Note newNote, Guid accountId)
        {
            var response = await _notesGateway.CreateNote(newNote, accountId).ConfigureAwait(false);

            return response;
        }
    }
}
