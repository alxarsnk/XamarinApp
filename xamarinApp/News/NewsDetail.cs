using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace xamarinApp
{
    public class NewsDetail : ContentPage
    {
        Article article = new Article();

        ToolbarItem toolBarItem = new ToolbarItem
        {
            Text = "Add",
            Order = ToolbarItemOrder.Primary,
            Priority = 0
        };

        public NewsDetail(Article article)
        {
            Title = "Новость";
            this.article = article;

            var image = new Image();
            try
            {
                image.Source = ImageSource.FromUri(new Uri(article.urlToImage ?? "http://www.contoso.com/"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var authorLabel = new Label { Text = (string)article.author };
            var titleLabel = new Label { Text = (string)article.title };
            var descLabel = new Label { Text = (string)article.description };
            var content = new Label { Text = (string)article.content };
            var publishedTImeLabel = new Label { Text = (string)article.publishedAt };
            var urlLabel = new Label { Text = (string)article.url };

            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = new StackLayout
                {
                    Children = {

                    new Frame
                    {
                        Content = image,
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children = {
                                new Label { Text = "Author: ", FontAttributes = FontAttributes.Bold },
                                authorLabel

                            },
                        Orientation = StackOrientation.Horizontal,
                        Padding = 4
                       },
                        BorderColor = Color.Gray,
                        BackgroundColor = Color.FromHex("#e1e1e1"),
                        CornerRadius = 12
                    },
                    //new Frame
                    //{
                    //    Content =new StackLayout
                    //    {
                    //        Children = {
                    //            new Label { Text = "Source: ", FontAttributes = FontAttributes.Bold },
                    //            articleSourceName
                    //        },
                    //    Orientation = StackOrientation.Horizontal,
                    //    Padding = 4
                    //   },
                    //    BorderColor = Color.Gray,
                    //    BackgroundColor = Color.FromHex("#e1e1e1"),
                    //    CornerRadius = 12
                    //},
                    new Frame
                    {
                        Content = new StackLayout
                        {
                            Children = {
                                new Label { Text = "Title: ", FontAttributes = FontAttributes.Bold },
                                titleLabel
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
                                new Label { Text = "Description: ", FontAttributes = FontAttributes.Bold },
                                descLabel
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
                                new Label { Text = "Content: ", FontAttributes = FontAttributes.Bold },
                                content
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
                                new Label { Text = "Published: ", FontAttributes = FontAttributes.Bold },
                                publishedTImeLabel
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
                                new Label { Text = "URL: ", FontAttributes = FontAttributes.Bold },
                                urlLabel
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
                }
            };
            Content = scrollView;
            toolBarItem.Clicked += settingsCLicked;
            this.ToolbarItems.Add(toolBarItem);
            setupToolBarText();
        }

        public async void setupToolBarText()
        {
            toolBarItem.Text = await ItemIsSaved(article.id) ? "Remove" : "Add";
        }

        public async Task<bool> ItemIsSaved(int uniqId)
        {
            return await App.Database.GetArticleAsync(uniqId) != null;
        }

        public async void settingsCLicked(object sender, EventArgs e)
        {
            if (await ItemIsSaved(article.id))
            {
                await App.Database.DeleteArticleAsync(article);
            }
            else
            {
                await App.Database.SaveArticleAsync(article);
            }
            setupToolBarText();
        }
    }
}

