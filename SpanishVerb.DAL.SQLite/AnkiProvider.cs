using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpanishVerb.DAL.Abstract;

namespace SpanishVerb.DAL.SQLite
{
    public class AnkiProvider : IStorageProvider
    {

        private string _connectionString;
        public AnkiProvider(string connectionString)
        {
            _connectionString = connectionString;
            //_model = new AnkiEntities();
        }

        public IEnumerable<Note> GetAllVerbs()
        {
            using (var db = new AnkiEntities(_connectionString))
            {
                var verbs = db.notes.Where(n => n.tags.Contains("verbos")).OrderByDescending(v => v.id).Select(v => v);
                var single = verbs.First();

                var result = db.notes.Where(n => n.tags.Contains("verbos"));
                var count = result.Count();
                count = result.Where(n => n.tags.Contains("verbos")).Count();
                var list = result.Where(n => n.tags.Contains("verbos")).ToList();
                return db.notes.Where(n => n.tags.Contains("verbos")).ToList().Select(n =>
                {
                    return MapNote(n);
                }
                        );
            }
        }

        private static Note MapNote(note n)
        {
            string[] notes = n.flds.Split('\u001f');
            return new Note()
            {
                Front = notes[0],
                Back = notes[1],
                Details = notes[2],
                Id = n.id
            };
        }

        public void SaveNote(Note note)
        {
            using (var db = new AnkiEntities(_connectionString))
            {
                ////db.Database.Connection.
                var tempNote = db.notes.Where(n => n.id == note.Id).First();
                if (tempNote == null)
                {
                    tempNote = new note();
                }

                tempNote.flds = string.Join('\u001f'.ToString(), new string[] { note.Front, note.Back, note.Details });
                db.SaveChanges();
            }
        }
    }
}
