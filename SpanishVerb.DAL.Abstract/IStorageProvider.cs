using System.Collections.Generic;
using SpanishVerb.DAL.Abstract;

namespace SpanishVerb.DAL.Abstract
{
    public interface IStorageProvider
    {
        IEnumerable<Note> GetAllVerbs();
        void SaveNote(Note note);
    }
}