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
using System.Windows.Input;

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

            ToolbarItem item = new ToolbarItem
            {
                Text = "Saved",
                Order = ToolbarItemOrder.Primary,
                Priority = 0
            };
            item.Clicked += settingsCLicked;
            this.ToolbarItems.Add(item);


        }

        public async void settingsCLicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SavedPage());
        }

        public async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            User selectedUser = e.Item as User;
            ((ListView)sender).SelectedItem = null;
            await Navigation.PushAsync(new MyPage(selectedUser));
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
                    ItemTemplate = new DataTemplate(typeof(CustomCell))
                };

                listView.ItemTapped += OnItemTapped;
                this.Content = new StackLayout { Children = { listView } };
            }

        }
    }

}

public class User
{
    public int userId { get; set; }
    [SQLite.PrimaryKey]
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
        StackLayout horizontalLayout = new StackLayout();
        Label left = new Label();
        Label right = new Label();

        //set bindings
        left.SetBinding(Label.TextProperty, "title");
        right.SetBinding(Label.TextProperty, "body");
        image.SetBinding(Image.SourceProperty, "image");

        //Set properties for desired design
        horizontalLayout.Orientation = StackOrientation.Vertical;
        Frame frame = new Frame
        {
            Content = horizontalLayout,
            BorderColor = Color.Gray,
            BackgroundColor = Color.FromHex("#e1e1e1"),
            CornerRadius = 12
        };
        right.HorizontalOptions = LayoutOptions.EndAndExpand;
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
