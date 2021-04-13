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
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
        }

        public async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Default);
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


//    {
//   "documentation" : "https://opencagedata.com/api",
//   "licenses" : [
//      {
//         "name" : "see attribution guide",
//         "url" : "https://opencagedata.com/credits"
//      }
//   ],
//   "rate" : {
//    "limit" : 2500,
//      "remaining" : 2473,
//      "reset" : 1613001600
//   },
//   "results" : [
//      {
//         "annotations" : {
//            "DMS" : {
//               "lat" : "22\u00b0 40' 45.05736'' S",
//               "lng" : "14\u00b0 31' 36.48576'' E"
//            },
//            "MGRS" : "33KVQ5139191916",
//            "Maidenhead" : "JG77gh36fx",
//            "Mercator" : {
//    "x" : 1617116.157,
//               "y" : -2576798.589
//            },
//            "OSM" : {
//    "edit_url" : "https://www.openstreetmap.org/edit?node=4488973891#map=16/-22.67918/14.52680",
//               "note_url" : "https://www.openstreetmap.org/note/new#map=16/-22.67918/14.52680&layers=N",
//               "url" : "https://www.openstreetmap.org/?mlat=-22.67918&mlon=14.52680#map=16/-22.67918/14.52680"
//            },
//            "UN_M49" : {
//    "regions" : {
//        "AFRICA" : "002",
//                  "NA" : "516",
//                  "SOUTHERN_AFRICA" : "018",
//                  "SUB-SAHARAN_AFRICA" : "202",
//                  "WORLD" : "001"
//               },
//               "statistical_groupings" : [
//                  "LEDC"
//               ]
//            },
//            "callingcode" : 264,
//            "currency" : {
//    "alternate_symbols" : [
//                  "N$"
//               ],
//               "decimal_mark" : ".",
//               "disambiguate_symbol" : "N$",
//               "format" : "%n %u",
//               "html_entity" : "$",
//               "iso_code" : "NAD",
//               "iso_numeric" : "516",
//               "name" : "Namibian Dollar",
//               "smallest_denomination" : 5,
//               "subunit" : "Cent",
//               "subunit_to_unit" : 100,
//               "symbol" : "$",
//               "symbol_first" : 0,
//               "thousands_separator" : ","
//            },
//            "flag" : "\ud83c\uddf3\ud83c\udde6",
//            "geohash" : "k7fqfx6h5jbq5tn8tnpn",
//            "qibla" : 31.02,
//            "roadinfo" : {
//    "drive_on" : "left",
//               "road" : "Woermann St",
//               "speed_in" : "km/h"
//            },
//            "sun" : {
//    "rise" : {
//        "apparent" : 1612932540,
//                  "astronomical" : 1612927800,
//                  "civil" : 1612931160,
//                  "nautical" : 1612929480
//               },
//               "set" : {
//        "apparent" : 1612978980,
//                  "astronomical" : 1612983720,
//                  "civil" : 1612980360,
//                  "nautical" : 1612982040
//               }
//},
//            "timezone" : {
//    "name" : "Africa/Windhoek",
//               "now_in_dst" : 0,
//               "offset_sec" : 7200,
//               "offset_string" : "+0200",
//               "short_name" : "CAT"
//            },
//            "what3words" : {
//    "words" : "integrate.laughter.teller"
//            }
//         },
//         "bounds" : {
//    "northeast" : {
//        "lat" : -22.6791326,
//               "lng" : 14.5268516
//            },
//            "southwest" : {
//        "lat" : -22.6792326,
//               "lng" : 14.5267516
//            }
//},
//         "components" : {
//    "ISO_3166-1_alpha-2" : "NA",
//            "ISO_3166-1_alpha-3" : "NAM",
//            "_category" : "commerce",
//            "_type" : "restaurant",
//            "city" : "Swakopmund",
//            "continent" : "Africa",
//            "country" : "Namibia",
//            "country_code" : "na",
//            "postcode" : "13001",
//            "restaurant" : "Beryl's Restaurant",
//            "road" : "Woermann St",
//            "state" : "Erongo Region",
//            "suburb" : "Central"
//         },
//         "confidence" : 9,
//         "formatted" : "Beryl's Restaurant, Woermann St, Swakopmund 13001, Namibia",
//         "geometry" : {
//    "lat" : -22.6791826,
//            "lng" : 14.5268016
//         }
//      }
//   ],
//   "status" : {
//    "code" : 200,
//      "message" : "OK"
//   },
//   "stay_informed" : {
//    "blog" : "https://blog.opencagedata.com",
//      "twitter" : "https://twitter.com/OpenCage"
//   },
//   "thanks" : "For using an OpenCage API",
//   "timestamp" : {
//    "created_http" : "Wed, 10 Feb 2021 06:57:06 GMT",
//      "created_unix" : 1612940226
//   },
//   "total_results" : 1
//}
