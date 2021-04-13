using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace xamarinApp
{
    public class SavedPage : ContentPage
    {
        public List<User> Users { get; set; }

        public SavedPage()
        {

            Title = "SavedItems";

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            setupListView();
        }

        public async void setupListView()
        {
            var task = App.Database.GetNotesAsync();
            task.Wait();
            Users = task.Result;
            ListView listView = new ListView
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemsSource = Users,
                ItemTemplate = new DataTemplate(typeof(CustomCell))
            };

            listView.ItemTapped += OnItemTapped;
            this.Content = new StackLayout { Children = { listView } };
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            User selectedUser = e.Item as User;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new MyPage(selectedUser));
        }
    }
}

