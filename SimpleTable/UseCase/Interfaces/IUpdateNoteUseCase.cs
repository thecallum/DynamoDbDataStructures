using SimpleTable.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTable.UseCase.Interfaces
{
    public interface IUpdateNoteUseCase
    {
        Task<Note> Execute(Guid id, Note newNote);
    }
}
