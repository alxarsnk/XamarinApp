using System;

using Xamarin.Forms;

namespace xamarinApp
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "SettingsPage" }
                }
            };
        }
    }
}

