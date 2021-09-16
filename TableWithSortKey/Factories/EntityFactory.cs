using TableWithSortKey.Domain;
using TableWithSortKey.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace TableWithSortKey.Factories
{
    public static class EntityFactory
    {
        public static Note ToDomain(this NoteDb entity)
        {
            return new Note
            {
                Title = entity.Title,
                Contents = entity.Contents,
                Created = entity.Created
            };
        }

        public static NoteDb ToDatabase(this Note domain, Guid? id = null)
        {
            return new NoteDb
            {
                Id = (Guid)((id == null) ? Guid.NewGuid() : id),
                Title = domain.Title,
                Contents = domain.Contents,
                Created = domain.Created
            };
        }

        public static string ToConsole(this Note domain, Guid? id)
        {
            var message = System.Environment.NewLine;

            message += $"========" + System.Environment.NewLine;
            message += $"Id: {id}" + System.Environment.NewLine;
            message += $"Title: {domain.Title}" + System.Environment.NewLine;
            message += $"Contents: {domain.Contents}" + System.Environment.NewLine;
            message += $"Created: {domain.Created}" + System.Environment.NewLine; ;
            message += $"========" + System.Environment.NewLine;

            return message;       
        }
    }
}
