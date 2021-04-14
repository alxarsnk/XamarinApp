using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace xamarinApp
{
    public class SavedNews : ContentPage
    {
        
        public List<Article> articles { get; set; }

        public SavedNews()
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
            var task = App.Database.GetArticlesAsync();
            task.Wait();
            articles = task.Result;
            ListView listView = new ListView
            {
                HasUnevenRows = true,
                SeparatorVisibility = SeparatorVisibility.None,
                ItemsSource = articles,
                ItemTemplate = new DataTemplate(typeof(NewsCell))
            };

            listView.ItemTapped += OnItemTapped;
            this.Content = new StackLayout { Children = { listView } };
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Article selectedArticle = e.Item as Article;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new NewsDetail(selectedArticle));
        }
    }
}

