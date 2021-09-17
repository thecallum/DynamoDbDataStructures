using TableWithSecondaryIndexes.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSecondaryIndexes.UseCase.Interfaces
{
    public interface IGetAllNotesUseCase
    {
        Task<IEnumerable<Note>> Execute(Guid accountId);
    }
}
