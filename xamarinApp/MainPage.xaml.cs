using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net.Http;
using System.Net;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace xamarinApp
{
    public partial class MainPage : ContentPage
    {
        public List<User> Users { get; set; }

        public MainPage()
        {
            Title = "News";
            
            string jsonlink = @"https://jsonplaceholder.typicode.com/posts";
            makeRequest(jsonlink);
            
 
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            User selectedPhone = e.Item as User;
        }

        public async void makeRequest(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                ObservableCollection<User> list = JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
                Users = new List<User>(list);
                ListView listView = new ListView
                {
                    HasUnevenRows = true,
                    SeparatorVisibility = SeparatorVisibility.None,
                    ItemsSource = Users,
                    RowHeight = 200,
                    // Определяем формат отображения данных
                    ItemTemplate = new DataTemplate(() =>
                    {
                        ImageCell imageCell = new ImageCell { TextColor = Color.Black, DetailColor = Color.Gray };
                        imageCell.SetBinding(ImageCell.TextProperty, "title");
                        imageCell.SetBinding(ImageCell.DetailProperty, "body");
                        imageCell.SetBinding(ImageCell.ImageSourceProperty, "ImagePath");       
                        return imageCell;
                    })
                };
                listView.ItemTapped += OnItemTapped;
                this.Content = new StackLayout { Children = { listView } };
            }
            
        }
    }

}

public class Phone
{
    public string Title { get; set; }
    public string ImagePath { get; set; }
    public string Company { get; set; }
    public int Price { get; set; }
}

public class User
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}


public class CustomCell : ViewCell
{
    public CustomCell()
    {
        //instantiate each of our views
        var image = new Image();
        StackLayout cellWrapper = new StackLayout();
        StackLayout horizontalLayout = new StackLayout();
        View containerView = new View();
        Label title = new Label();
        Label body = new Label();

        //set bindings
        title.SetBinding(Label.TextProperty, "title");
        body.SetBinding(Label.TextProperty, "body");
        image.SetBinding(Image.SourceProperty, "image");

        //Set properties for desired design
        horizontalLayout.BackgroundColor = Color.LightPink;
        horizontalLayout.Orientation = StackOrientation.Horizontal;
        body.HorizontalOptions = LayoutOptions.EndAndExpand;
        title.TextColor = Color.Black;
        body.TextColor = Color.Gray;

        //add views to the view hierarchy
        horizontalLayout.Children.Add(image);
        horizontalLayout.Children.Add(title);
        horizontalLayout.Children.Add(body);
        cellWrapper.Children.Add(horizontalLayout);
        View = cellWrapper;
    }
}
