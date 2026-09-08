namespace naidis_TARge25;

public partial class TextPage : ContentPage
{
	Label lbl;
	Editor editor;
	HorizontalStackLayout hsl;
	VerticalStackLayout vsl;
	List<string> nupud = new List<string>() { "Tagasi", "Avalehele", "Edasi" };

	public TextPage()
	{
		lbl = new Label
		{
			Text = "Pealkiri",
			FontSize = 30,
			FontFamily = "Huxtable",
			TextColor = Colors.LightSalmon,
			HorizontalOptions = LayoutOptions.Center,
			FontAttributes = FontAttributes.Bold
		};

		editor = new Editor
		{
			Placeholder = "Sisesta tekst siia...",
			PlaceholderColor = Colors.DarkSalmon,
			FontSize = 20,
			FontFamily = "Huxtable",
			HorizontalOptions = LayoutOptions.Center,
			FontAttributes = FontAttributes.Italic,
			Keyboard = Keyboard.Text
		};

		editor.TextChanged += (s, e) =>
		{
			lbl.Text = editor.Text;
		};

        Button speechButton = new Button
        {
            Text = "Loe Ette",
            FontSize = 22,
            FontFamily = "Huxtable",
            BackgroundColor = Colors.Plum,
            TextColor = Colors.LightSalmon,
            CornerRadius = 10
        };

        speechButton.Clicked += Nupp_Clicked;

        hsl = new HorizontalStackLayout { Spacing = 20, HorizontalOptions = LayoutOptions.Center};
		 for (int i = 0; i < nupud.Count; i++)
		{
			Button nupp = new Button
			{
				Text = nupud[i],
				FontSize = 20,
				FontFamily = "Huxtable",
				HorizontalOptions = LayoutOptions.Center,
				FontAttributes = FontAttributes.Bold,
				BackgroundColor = Colors.Fuchsia,
				TextColor = Colors.Plum,
                CornerRadius = 10,
                HeightRequest = 50,
                ZIndex = i
			};
			hsl.Add(nupp); // Lisame nupu horisontaalne virnastus
			nupp.Clicked += Nupp_Clicked;
		}

		vsl = new VerticalStackLayout
		{
			Padding = 20,
			Spacing = 20,
            Children = { lbl, editor, speechButton, hsl },
			HorizontalOptions = LayoutOptions.Center
		};
		Content = vsl;
	}

	private void Nupp_Clicked(object? sender, EventArgs e)
	{
		Button nupp = sender as Button;
		if (nupp.ZIndex == 0)
		{
			Navigation.PopAsync(); // Tagasi lehele
		}
		else if (nupp.ZIndex == 1)
		{
			Navigation.PopToRootAsync();
		}
		else if (nupp.ZIndex == 2)
		{
			Navigation.PushAsync(new FigurePage()); // Edasi lehele
		}
		
	}

    private async void Btn_Clicked(object? sender, EventArgs e)
    {
        IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

        SpeechOptions options = new SpeechOptions()
        {
            Pitch = 1.5f, // 0.0 - 2.0
            Volume = 0.75f, // 0.0 - 1.0
            Locale = locales.FirstOrDefault()
        };
        string? text = editor.Text;
        if (string.IsNullOrWhiteSpace(text))
        {
            await DisplayAlert("Viga", "Palun sisesta tekst", "Ok");
            return;
        }
        try
        {
            await TextToSpeech.SpeakAsync(text, options);
        }
        catch (Exception ex)
        {
            await DisplayAlert("TTS viga", ex.Message, "OK");
        }

    }
}