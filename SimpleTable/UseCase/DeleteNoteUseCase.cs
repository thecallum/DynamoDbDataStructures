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
    public class DeleteNoteUseCase : IDeleteNoteUseCase
    {
        private NotesDbGateway _notesGateway;

        public DeleteNoteUseCase(IDynamoDBContext dynamoDbContext)
        {
            _notesGateway = new NotesDbGateway(dynamoDbContext);
        }

        public async Task<Note> Execute(Guid id)
        {
            var response = await _notesGateway.DeleteNote(id).ConfigureAwait(false);
            // returns null if not found

            return response;
        }
    }
}
