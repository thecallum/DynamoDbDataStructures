using SimpleTable.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleTable.Gateway
{
    public interface INotesDbGateway
    {
        Task<Note> GetNoteById(Guid id);
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Guid> CreateNote(Note newNote);
        Task<Note> UpdateNote(Guid id, Note newNote);
        Task<Note> DeleteNote(Guid id);
    }
}
