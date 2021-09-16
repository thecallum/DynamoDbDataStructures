using SimpleTable.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTable.UseCase.Interfaces
{
    public interface IGetNoteByIdUseCase
    {
        Task<Note> Execute(Guid id);
    }
}
