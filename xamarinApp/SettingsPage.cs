using System;

using Xamarin.Forms;

namespace xamarinApp
{
    public class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            Title = "Menu";

            // Your label tap event
            var savedTap = new TapGestureRecognizer();
            savedTap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new MainPage());
            };
            var savedButton = new StackLayout
            {
                Children = {
                    new Image { Source = "temporary.png", WidthRequest = 24, HeightRequest = 24 },
                    new Label { Text = "Тестовое API" }
                },
                Orientation = StackOrientation.Horizontal,
                Padding = 4,
            };
            savedButton.GestureRecognizers.Add(savedTap);

            var locationTap = new TapGestureRecognizer();
            locationTap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new LocationPage());
            };
            var locationButton = new StackLayout
            {
                Children = {
                    new Image { Source = "location.png", WidthRequest = 24, HeightRequest = 24 },
                   new Label { Text = "Текущее местоположение"}
                },
                Orientation = StackOrientation.Horizontal,
                Padding = 4,
            };
            locationButton.GestureRecognizers.Add(locationTap);

            var newsTap = new TapGestureRecognizer();
            newsTap.Tapped += (s, e) =>
            {
                Navigation.PushAsync(new NewsPage());
            };
            var newsButton = new StackLayout
            {
                Children = {
                    new Image { Source = "news.png", WidthRequest = 24, HeightRequest = 24 },
                   new Label { Text = "Новости"}
                },
                Orientation = StackOrientation.Horizontal,
                Padding = 4,
            };
            newsButton.GestureRecognizers.Add(newsTap);


            Content = new StackLayout
            {
                Children = {
                    new Frame
                    {
                        Content = savedButton,
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content = locationButton,
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content = newsButton,
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    }
                },
                Padding = 16
            };
        }
    }
}

