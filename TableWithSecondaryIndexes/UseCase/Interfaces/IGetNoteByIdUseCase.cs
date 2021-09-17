using TableWithSecondaryIndexes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSecondaryIndexes.UseCase.Interfaces
{
    public interface IGetNoteByIdUseCase
    {
        Task<Note> Execute(Guid noteId, Guid accountId);
    }
}
