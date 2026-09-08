using Microsoft.Maui.Controls.Shapes;

namespace Valgusfoor;

public partial class ValgusfoorPage : ContentPage
{
    BoxView punane;
    BoxView kollane;
    BoxView roheline;

    Button onBtn;
    Button offBtn;

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

        // Kõik ekraanile
        VerticalStackLayout page = new VerticalStackLayout
        {
            Spacing = 20,
            Padding = 20,
            Children =
            {
                vsl,
                hsl
            }
        };

        Content = page;
    }

    private void OnBtn_Clicked(object sender, EventArgs e)
    {
        punane.Color = Colors.Red;
        kollane.Color = Colors.Yellow;
        roheline.Color = Colors.Green;
    }

    private void OffBtn_Clicked(object sender, EventArgs e)
    {
        punane.Color = Colors.Gray;
        kollane.Color = Colors.Gray;
        roheline.Color = Colors.Gray;
    }
}