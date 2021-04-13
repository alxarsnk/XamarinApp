using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xamarinApp
{
    public class MyPage : ContentPage
    {
        User user = new User();
        ToolbarItem toolBarItem = new ToolbarItem
        {
            Text = "Add",
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };

        public MyPage(User user)
        {
            Title = $"{user.id}";
            this.user = user;


            Content = new StackLayout
            {
                Children = {
                    new Frame
                    {
                        Content =new StackLayout
                        {
                            Children = {
                                new Label { Text = "Id: ", FontAttributes = FontAttributes.Bold },
                                new Label { Text = user.id.ToString() }
                            },
                        Orientation = StackOrientation.Horizontal,
                        Padding = 4
                       },
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content =new StackLayout
                        {
                            Children = {
                                new Label { Text = "User Id: ", FontAttributes = FontAttributes.Bold },
                                new Label { Text = user.userId.ToString() }
                            },
                        Orientation = StackOrientation.Horizontal,
                        Padding = 4
                       },
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children = {
                                new Label { Text = "Title: ", FontAttributes = FontAttributes.Bold },
                                new Label { Text = user.title.ToString() }
                            },
                        Orientation = StackOrientation.Vertical,
                        Padding = 4
                       },
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children = {
                                new Label { Text = "Body: ", FontAttributes = FontAttributes.Bold },
                                new Label { Text = user.body.ToString() },
                            },
                            Orientation = StackOrientation.Vertical,
                            Padding = 4
                        },
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },

                },
                Padding = 16
            };


            toolBarItem.Clicked += settingsCLicked;
            this.ToolbarItems.Add(toolBarItem);
            setupToolBarText();
        }

        public async void setupToolBarText()
        {
            toolBarItem.Text = await ItemIsSaved(user.id) ? "Remove" : "Add";
        }

        public async Task<bool> ItemIsSaved(int uniqId)
        {
            return await App.Database.GetNoteAsync(uniqId) != null;
        }

        public async void settingsCLicked(object sender, EventArgs e)
        {
            if (await ItemIsSaved(user.id))
            {
                await App.Database.DeleteNoteAsync(user);
            }
            else
            {
                await App.Database.SaveNoteAsync(user);
            }
            setupToolBarText();
        }
    }
}

