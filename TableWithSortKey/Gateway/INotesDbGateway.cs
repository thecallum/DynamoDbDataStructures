using TableWithSortKey.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TableWithSortKey.Gateway
{
    public interface INotesDbGateway
    {
        Task<Note> GetNoteById(Guid noteId, Guid accountId);
        Task<IEnumerable<Note>> GetAllNotes(Guid accountId);
        Task<Guid> CreateNote(Note newNote, Guid accountId);
        Task<Note> UpdateNote(Guid noteId, Guid accountId, Note newNote);
        Task<Note> DeleteNote(Guid noteId, Guid accountId);
    }
}
