using System.Net;
using System.Net.Http;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;

namespace xamarinApp
{
    public class LocationPage : ContentPage
    {
        double Latitude;
        double Longitude;
        Label lat = new Label { Text = "Широта: опеределяется.." };
        Label lon = new Label { Text = "Долгота: опеределяется.." };
        Label errorMessage = new Label { IsVisible = false };
        public LocationPage()
        {
            Content = new StackLayout
            {
                Children = {
                    lat,
                    lon,
                    errorMessage
                },
                Padding = 12
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
        }

        public async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                lat.Text = $" Широта: {location.Latitude}";
                lon.Text = $" Долгота: {location.Longitude}";
                Latitude = location.Latitude;
                Longitude = location.Longitude;
                getCity();
            }
            else
            {
                lat.IsVisible = false;
                lon.IsVisible = false;
                errorMessage.IsVisible = true;
                errorMessage.Text = "Не удалось определить координаты";
            }
        }

        public async void getCity()
        {
            string request = $"https://api.opencagedata.com/geocode/v1/json?q={Latitude}+{Longitude}&key=1dc3b1ff946b459982d408e968d27642";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                var dict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
                var result = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>($"{dict["results"][0]}");
                var formated = $"{result["formatted"]}";
                errorMessage.IsVisible = true;
                errorMessage.Text = formated;
            }
        }
    }
}