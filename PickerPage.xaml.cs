using Microsoft.Maui.Layouts;

namespace naidis_TARge25;

public partial class PickerPage : ContentPage
{
	Picker picker;
	AbsoluteLayout absLayout;
	DatePicker datePicker;
	TimePicker timePicker;
	Label datetime_label;
	public PickerPage()
	{
		picker = new Picker
		{
			Title = "Vali lemmikvärv",
			ItemsSource = new List<string> { "Punane", "Roheline", "Sinine" },
			HorizontalOptions = LayoutOptions.Center
		};
		picker.SelectedIndexChanged += (s, e) =>
		{
			if (picker.SelectedIndex != -1)
			{
				switch (picker.SelectedIndex)
				{
					case 0:
						BackgroundColor = Colors.IndianRed;
						break;
					case 1:
						BackgroundColor = Colors.SeaGreen;
						break;
					case 2:
						BackgroundColor = Colors.LightSkyBlue;
						break;
					default:
						break;
				}
			}
			else
			{
				DisplayAlertAsync("Viga", "Vali värv", "OK");
			}
		};

		datetime_label = new Label
		{
			Text = "Vali kuupäev: ",
			FontSize = 20,
			HorizontalOptions = LayoutOptions.Center
		};

		datePicker = new DatePicker
		{
			MaximumDate = DateTime.Now.AddDays(15),
			MinimumDate = DateTime.Now.AddYears(-100),
			Date = DateTime.Now,
			Format = "D",
			HorizontalOptions = LayoutOptions.Center
		};

		datePicker.DateSelected += (s, e) =>
		{
			datetime_label.Text = $"Valitud kuupäev: {e.NewDate:D}";
		};

		timePicker = new TimePicker
		{
			Time = DateTime.Now.TimeOfDay,
			Format = "t",
			HorizontalOptions = LayoutOptions.Center
		};
		timePicker.PropertyChanged += (s, e) =>
		{
			if (e.PropertyName == nameof(TimePicker.Time))
			{
				datetime_label.Text = $"Valitud kellaaeg: {timePicker.Time}";
			}
		};


		absLayout = new AbsoluteLayout();
		List<View> views = new List<View> { picker, datePicker, timePicker, datetime_label }; 
		for (int i = 0; i < views.Count; i ++)
		{
			absLayout.Children.Add(views[i]);
			double yKoht = 0.1 + i * 0.2; //yKoht määrab, kui kaugel elemendid üksteisest on
			AbsoluteLayout.SetLayoutBounds(views[i], new Rect(0.5, yKoht, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
			AbsoluteLayout.SetLayoutFlags(views[i], AbsoluteLayoutFlags.PositionProportional);
		}
		Content = absLayout;
	}
}