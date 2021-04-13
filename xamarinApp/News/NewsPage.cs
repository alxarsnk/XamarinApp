using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace xamarinApp
{
    public class NewsPage : ContentPage
    {
        public List<Article> articles { get; set; }

        public NewsPage()
        {
            Title = "News";
            makeRequest("https://newsapi.org/v2/top-headlines?country=ru&category=business&apiKey=59e4bf72cd8147c39800444c7a29aa27");
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Article selectedAtricle = e.Item as Article;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new NewsDetail(selectedAtricle));
        }

        public async void makeRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                var fetchedArticles = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                ObservableCollection<Article> list = JsonConvert.DeserializeObject<ObservableCollection<Article>>($"{fetchedArticles["articles"]}");
                articles = new List<Article>(list);

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
        }
    }
}

public class Article
{
    public Source source { get; set; }
    public string author { get; set; }
    public string title { get; set; }
    public string description { get; set; }
    public string url { get; set; }
    public string urlToImage { get; set; }
    public string publishedAt { get; set; }
    public string content { get; set; }
    [SQLite.PrimaryKey]
    public int id
    {
        get
        {
            return (int)Math.Sqrt(title.GetHashCode() * title.GetHashCode());
        }
        set
        {
            Math.Sqrt(title.GetHashCode() * title.GetHashCode());
        }
    }
}

public class Source
{
    public string id { get; set; }
    public string name { get; set; }
}

public class NewsCell : ViewCell
{
    public NewsCell()
    {
        //instantiate each of our views
        var image = new Image();
        StackLayout horizontalLayout = new StackLayout();
        Label left = new Label();
        Label right = new Label();

        //set bindings
        left.SetBinding(Label.TextProperty, "title");
        right.SetBinding(Label.TextProperty, "source.name");
        image.SetBinding(Image.SourceProperty, "urlToImage");
        ForceUpdateSize();
        //Set properties for desired design
        horizontalLayout.Orientation = StackOrientation.Vertical;
        Frame frame = new Frame
        {
            Content = horizontalLayout,
            BorderColor = Color.Gray,
            BackgroundColor = Color.FromHex("#e1e1e1"),
            CornerRadius = 12
        };
        right.HorizontalOptions = LayoutOptions.StartAndExpand;
        left.TextColor = Color.Black;
        right.TextColor = Color.Gray;

        //add views to the view hierarchy
        horizontalLayout.Children.Add(image);
        horizontalLayout.Children.Add(left);
        horizontalLayout.Children.Add(right);
        StackLayout stackLayout = new StackLayout() { Children = { frame }, Padding = 16 };
        View = stackLayout;
    }
}