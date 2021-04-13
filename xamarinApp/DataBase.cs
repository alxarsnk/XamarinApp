using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;

namespace xamarinApp
{
    public class NoteDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<User>().Wait();
        }

        public Task<List<User>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<User>().ToListAsync();
        }

        public Task<User> GetNoteAsync(int uniqId)
        {
            // Get a specific note.
            return database.Table<User>()
                            .Where(i => i.id == uniqId)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(User note)
        {
           
                Console.WriteLine($"Save a new note. {note.id}");
                // Save a new note.
                return database.InsertAsync(note);
        
        }

        public Task<int> DeleteNoteAsync(User note)
        {
            Console.WriteLine("Delete a note.");
            // Delete a note.
            return database.DeleteAsync(note);
        }
    }
}
