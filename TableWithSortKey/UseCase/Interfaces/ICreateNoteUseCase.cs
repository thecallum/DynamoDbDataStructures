using TableWithSortKey.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TableWithSortKey.UseCase.Interfaces
{
    public interface ICreateNoteUseCase
    {
        Task<Guid> Execute(Note newNote);
    }
}
