using Valgusfoor;

namespace naidis_TARge25;

public partial class StartPage : ContentPage
{
    VerticalStackLayout vsl;

    public List<ContentPage> Lehed = new List<ContentPage>()
    {
        new TextPage(),
        new ValgusfoorPage()
    };

    public List<string> Lehenimed = new List<string>()
    {
        "Tekst",
        "Valgusfoor"
    };

    public StartPage()
    {
        vsl = new VerticalStackLayout
        {
            Padding = 20,
            Spacing = 20
        };

        for (int i = 0; i < Lehed.Count; i++)
        {
            Button nupp = new Button
            {
                Text = Lehenimed[i],
                FontSize = 20,
                FontFamily = "Huxtable",
                BackgroundColor = Colors.LightBlue,
                TextColor = Colors.White,
                CornerRadius = 10,
                HeightRequest = 60,
                ZIndex = i
            };

            vsl.Add(nupp);

            nupp.Clicked += (s, e) =>
            {
                var valik = Lehed[nupp.ZIndex];
                Navigation.PushAsync(valik);
            };
        }

        Content = vsl;
    }
}