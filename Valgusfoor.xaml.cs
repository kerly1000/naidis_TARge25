using Microsoft.Maui.Controls.Shapes;

namespace Valgusfoor;

public partial class ValgusfoorPage : ContentPage
{
    BoxView punane;
    BoxView kollane;
    BoxView roheline;
    Button onBtn;
    Button offBtn;
    Label valgusLabel;

    bool foorOn = false;

    public ValgusfoorPage()
    {
        // Punane tuli
        punane = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 50
        };

        // Kollane tuli
        kollane = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 50
        };

        // Roheline tuli
        roheline = new BoxView
        {
            Color = Colors.Gray,
            WidthRequest = 100,
            HeightRequest = 100,
            HorizontalOptions = LayoutOptions.Center,
            CornerRadius = 50
        };

        // Alguses ei saa tulesid klõpsata
        punane.IsEnabled = false;
        kollane.IsEnabled = false;
        roheline.IsEnabled = false;

        // Pealkiri / tekst
        valgusLabel = new Label
        {
            Text = "Vali valgus",
            FontSize = 24,
            HorizontalOptions = LayoutOptions.Center
        };

        // Punase tule klõps
        punane.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                if (foorOn)
                {
                    valgusLabel.Text = "Seisa!";
                    punane.Color = Colors.Red;
                    kollane.Color = Colors.Grey;
                    roheline.Color = Colors.Grey;
                }
            })
        });

        // Kollase tule klõps
        kollane.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                if (foorOn)
                    
                {
                    valgusLabel.Text = "Ole valmis!";
                    punane.Color = Colors.Grey;
                    kollane.Color = Colors.Yellow;
                    roheline.Color = Colors.Grey;
                }
            })
        });

        // Rohelise tule klõps
        roheline.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(() =>
            {
                if (foorOn)
                    
                {
                    valgusLabel.Text = "Sõida!";
                    punane.Color = Colors.Grey;
                    kollane.Color = Colors.Grey;
                    roheline.Color = Colors.Green;
                }
            })
        });

        // ON nupp
        onBtn = new Button
        {
            Text = "On"
        };

        // OFF nupp
        offBtn = new Button
        {
            Text = "Off"
        };

        // ON nupu tegevus
        onBtn.Clicked += OnBtn_Clicked;

        // OFF nupu tegevus
        offBtn.Clicked += OffBtn_Clicked;

        // Valgusfoor
        VerticalStackLayout vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 15,
            HorizontalOptions = LayoutOptions.Center,
            Children =
        {
            punane,
            kollane,
            roheline
        }
        };

        // Nupud kõrvuti
        HorizontalStackLayout hsl = new HorizontalStackLayout
        {
            Spacing = 20,
            HorizontalOptions = LayoutOptions.Center,
            Children =
    {
        onBtn,
        offBtn
    }
        };

        // Taustapilt
        Grid page = new Grid();

        Image backgroundImage = new Image
        {
            Source = "linn.jpg",
            Aspect = Aspect.AspectFill
        };

        page.Children.Add(backgroundImage);

        // Foori sisu taustapildi peale
        VerticalStackLayout content = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = 20,
            Children =
    {
        valgusLabel,
        vsl,
        hsl
    }
        };

        page.Children.Add(content);

        Content = page;

    }

    private void OnBtn_Clicked(object sender, EventArgs e)
    {
        foorOn = true;

        punane.Color = Colors.Red;
        kollane.Color = Colors.Yellow;
        roheline.Color = Colors.Green;

        punane.IsEnabled = true;
        kollane.IsEnabled = true;
        roheline.IsEnabled = true;

        valgusLabel.Text = "Vali valgus";
    }

    private void OffBtn_Clicked(object sender, EventArgs e)
    {
        foorOn = false;

        punane.Color = Colors.Gray;
        kollane.Color = Colors.Gray;
        roheline.Color = Colors.Gray;

        punane.IsEnabled = false;
        kollane.IsEnabled = false;
        roheline.IsEnabled = false;

        valgusLabel.Text = "Lülita esmalt foor sisse";
    }


}