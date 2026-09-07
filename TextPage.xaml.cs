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
			Text = "Tekstileht",
			FontSize = 30,
			FontFamily = "Huxtable",
			HorizontalOptions = LayoutOptions.Center,
			FontAttributes = FontAttributes.Bold
		};
		editor = new Editor
		{
			Placeholder = "Sisesta tekst siia...",
			FontSize = 20,
			FontFamily = "Huxtable",
			HorizontalOptions = LayoutOptions.Center,
			FontAttributes = FontAttributes.Bold,
			Keyboard = Keyboard.Text
		};
		editor.TextChanged += (s, e) =>
		{
			lbl.Text = editor.Text;
		};
		hsl = new HorizontalStackLayout();
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
				ZIndex = i
			};
			hsl.Add(nupp); // Lisame nupu horisontaalne virnastus
			nupp.Clicked += Nupp_Clicked;
		}

		vsl = new VerticalStackLayout
		{
			Padding = 20,
			Spacing = 20,
            Children = { lbl, editor, hsl }
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
		else if (nupp.ZIndex == 3)
		{
			//Räägi nupp
		}
	}
}