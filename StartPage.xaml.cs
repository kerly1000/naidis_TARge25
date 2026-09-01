namespace naidis_TARge25;

public partial class StartPage : ContentPage
{
	VerticalStackLayout vst;
	public List<ContentPage> Lehed = new List<ContentPage>() { new TextPage(), new FigurePage() };
	public List<string> Lehenimed = new List<string>() { "Tekst", "Kujundus" };

	public StartPage()
	{
		vst = new VerticalStackLayout();
		for (int i=0; i<Lehed.Count; i++)
		{
			Button nupp = new Button
			{
				Text = Lehenimed[i],
				FontSize = 20,
				BackgroundColor = Colors.LightBlue,
				TextColor = Colors.White,
				CornerRadius = 10,
				ZIndex = i
			};
			nupp.Clicked += (s, e) =>
			{
				var valik = Lehed[nupp.ZIndex];
				Navigation.PushAsync(Lehed[i]);
			};
			vst.Add(nupp);
		}
		Content = vst;
	}
}