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
                    new Label { Text = user.id.ToString() },
                    new Label { Text = user.userId.ToString() },
                    new Label { Text = user.title.ToString() },
                    new Label { Text = user.body.ToString() },
                }
            };

            toolBarItem.Clicked += settingsCLicked;
            this.ToolbarItems.Add(toolBarItem);
            setupToolBarText();
        }

        public async void setupToolBarText()
        {
            toolBarItem.Text = await ItemIsSaved(user.id) ? "Remove" : "Add";
        }

        public async Task<bool> ItemIsSaved(int id)
        { 
            return await App.Database.GetNoteAsync(id) != null;
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

