using System;

using Xamarin.Forms;

namespace xamarinApp
{
    public class MyPage : ContentPage
    {
        public MyPage(User user)
        {
            Title = $"{user.id}";

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = user.id.ToString() },
                    new Label { Text = user.userId.ToString() },
                    new Label { Text = user.title.ToString() },
                    new Label { Text = user.body.ToString() },
                }
            };
        }
    }
}

