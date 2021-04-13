using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SQLite;

namespace xamarinApp
{
    public class NoteDatabase
    {
        SQLiteAsyncConnection database;

        public NoteDatabase(string dbPath)
        {
            CreateDb(dbPath);
        }

        public async void CreateDb(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            // wait until first query completed
            await database.CreateTableAsync<User>();
            // then execute second create query
            await database.CreateTableAsync<Article>();
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


        public Task<List<Article>> GetArticlesAsync()
        {
            //Get all articles.
            return database.Table<Article>().ToListAsync();
        }

        public Task<Article> GetArticleAsync(int uniqId)
        {
            // Get a specific article.
            return database.Table<Article>()
                            .Where(i => i.id == uniqId)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveArticleAsync(Article article)
        {

            Console.WriteLine($"Save a new article. {article.id}");
            // Save a new article.
            return database.InsertAsync(article);

        }

        public Task<int> DeleteArticleAsync(Article article)
        {
            Console.WriteLine("Delete a article.");
            // Delete a article.
            return database.DeleteAsync(article);
        }
    }
}
