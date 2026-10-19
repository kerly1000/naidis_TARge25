using naidis_TARge25.Resources.Languages;
using System.Collections.ObjectModel;
using System.Globalization;

namespace naidis_TARge25;

public partial class CarouselPage : ContentPage
{
    public class CarouselItem : BindableObject
    {
        public string TitleKey { get; set; }
        public string DescriptionKey { get; set; }
        public string Image { get; set; }

        
        public string Title => AppResources.ResourceManager.GetString(TitleKey, CultureInfo.CurrentUICulture) ?? TitleKey;
        public string Description => AppResources.ResourceManager.GetString(DescriptionKey, CultureInfo.CurrentUICulture) ?? DescriptionKey;

        public void RefreshLanguage()
        {
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }
    }

    private CarouselView carouselView;
    private IndicatorView indicatorView;
    private ObservableCollection<CarouselItem> items;
    private int position = 0;
    private bool isPopupOpen = false;
    private IDispatcherTimer timer;

    public CarouselPage()
    {
        BuildPageOnce();
        StartCarouselTimer();
    }

    private void BuildPageOnce()
    {
        

        UpdatePageTitle();

        //karuselli elementide andmed
        items = new ObservableCollection<CarouselItem>
        {
            new CarouselItem { TitleKey = "DogWorker", DescriptionKey = "WorkerDesc", Image = "bordercollie.jpg" },
            new CarouselItem { TitleKey = "DogLazy", DescriptionKey = "LazyDesc", Image = "boldog1.jpg" },
            new CarouselItem { TitleKey = "DogFight", DescriptionKey = "FighterDesc", Image = "malinois.jpg" },
            new CarouselItem { TitleKey = "DogRock", DescriptionKey = "RockDesc", Image = "chinesecreasted.jpg" },
            new CarouselItem { TitleKey = "DogClown", DescriptionKey = "ClownDesc", Image = "bullikas.jpg" }
        };

        //carouselView
        carouselView = new CarouselView
        {
            ItemsSource = items,
            HeightRequest = 350,
            PeekAreaInsets = new Thickness(40, 0, 40, 0),
            ItemTemplate = new DataTemplate(() =>
            {
                var frame = new Frame
                {
                    CornerRadius = 15,
                    HasShadow = true,
                    Padding = 0,
                    Margin = new Thickness(5),
                    BackgroundColor = Colors.Black
                };

                //popupi kontroll karuselli peatamiseks/käivitamiseks
                var tapGesture = new TapGestureRecognizer();  
                tapGesture.Tapped += async (s, e) =>
                {
                    isPopupOpen = true;
                    var boundItem = (s as View)?.BindingContext as CarouselItem;
                    if (boundItem != null)
                    {
                        await DisplayAlert(boundItem.Title, boundItem.Description, "OK");
                    }
                    isPopupOpen = false;
                };
                frame.GestureRecognizers.Add(tapGesture);

                var grid = new Grid();
                var image = new Image { Aspect = Aspect.AspectFill };
                image.SetBinding(Image.SourceProperty, "Image");

                var gradient = new BoxView
                {
                    Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 1),
                        EndPoint = new Point(0, 0),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop(Colors.Black.WithAlpha(0.7f), 0),
                            new GradientStop(Colors.Transparent, 1)
                        }
                    }
                };

                var label = new Label
                {
                    TextColor = Colors.Beige,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(15),
                    VerticalOptions = LayoutOptions.End
                };
                //teksti sidumine CarouselItem Title omadusega
                label.SetBinding(Label.TextProperty, "Title");

                grid.Children.Add(image);
                grid.Children.Add(gradient);
                grid.Children.Add(label);

                frame.Content = grid;
                return frame;
            })
        };

        indicatorView = new IndicatorView
        {
            IndicatorColor = Colors.Beige,
            SelectedIndicatorColor = Colors.DarkSlateBlue,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 10)
        };
        carouselView.IndicatorView = indicatorView;

        //keelevaliku nupud
        var btnEst = new Button { Text = "🇪🇪 Eesti", BackgroundColor = Colors.RoyalBlue, TextColor = Colors.LightGrey };
        btnEst.Clicked += (s, e) => ChangeLanguage("et");

        var btnEng = new Button { Text = "🇬🇧 English", BackgroundColor = Colors.RoyalBlue, TextColor = Colors.LightGray };
        btnEng.Clicked += (s, e) => ChangeLanguage("en");

        var langLayout = new HorizontalStackLayout
        {
            Spacing = 10,
            HorizontalOptions = LayoutOptions.Center,
            Children = { btnEst, btnEng }
        };

        BackgroundColor = Color.FromArgb("#1a1a2e");

        //elementide lehele paigutus
        Content = new ScrollView
        {
            Content = new VerticalStackLayout
            {
                Padding = 20,
                Spacing = 20,
                Children =
                {
                    langLayout,
                    carouselView,
                    indicatorView
                }
            }
        };
    }

    private void UpdatePageTitle()
    {
        Title = AppResources.ResourceManager.GetString("WindowTitle", CultureInfo.CurrentUICulture) ?? "Kui koerad oleksid inimesed";
    }

    private void StartCarouselTimer()
    {
        if (timer != null) return; //et poleks topelt taimereid

        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(5);
        timer.Tick += (s, e) =>
        {
            if (isPopupOpen || items == null || items.Count == 0) return;

            position = (position + 1) % items.Count;
            carouselView.Position = position;
        };
        timer.Start();
    }

    private void ChangeLanguage(string cultureCode)
    {
        var culture = new CultureInfo(cultureCode);
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;

        //lehe tiitli uuendamine
        UpdatePageTitle();

        //käsime kõikidel elementidel oma tekstid uuesti RESX-ist laadida
        foreach (var item in items)
        {
            item.RefreshLanguage();
        }
    }
}