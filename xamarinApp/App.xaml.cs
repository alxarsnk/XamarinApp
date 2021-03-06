using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace xamarinApp
{
    public partial class App : Application
    {
        static NoteDatabase database;

        // Create the database connection as a singleton.
        public static NoteDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new NoteDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NewDB.db3"));

                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
      
            MainPage = new NavigationPage( new SettingsPage());
            var _ = Database;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
