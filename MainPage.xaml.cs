namespace naidis_TARge25
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
            {
                CounterBtn.Text = $"Clicked {count} time";
            }
            else
            { 
                CounterBtn.Text = $"Clicked {count} times";
            }
            SemanticScreenReader.Announce(CounterBtn.Text);
            ResetBtn.Text = $"Tagasi nulli";
            dotnetBot.Scale -= 0.1; // Pilt muutub suuremaks
            dotnetBot.Opacity -= 0.1; // Pilt muutub läbipaistvamaks

            //Genereerime juhusliku värvi
            var random = new Random();
            var color = Color.FromRgb(random.Next(256), random.Next(256), random.Next(256)); //Loome juhusliku värvi
            ResetBtn.BackgroundColor = color; //Määrame nupu taustavärviks juhusliku värvi

            //elementide peitmine ja näitamine
            if (count % 10 == 0) //või count == 10
            {
                dotnetBot.IsVisible = false; //Peidame dotnetBot pildi, kui count on 10
            }
        }

        private void ResetBtn_Clicked(object sender, EventArgs e)
        {
            count = 0;
            CounterBtn.Text = $"Clicked {count} times";
            ResetBtn.Text = $"Reset tehtud!";
            dotnetBot.IsVisible = true; // Teeme pildi nähtavaks, kui count on 0
            dotnetBot.Scale += 1; // Teeme pildi suureks
            
            ResetBtn.ClearValue(Button.BackgroundColorProperty); // Eemaldame nupu värvi, et see taastakse algse värvi
        }

        private void Paremale_Vasakule_Clicked(object sender, EventArgs e)
        {
            if (dotnetBot.HorizontalOptions == LayoutOptions.Start)
            {
                dotnetBot.HorizontalOptions = LayoutOptions.End;//Kui on juba vasakul, liigume paremale
            }
            else
            {
                dotnetBot.HorizontalOptions = LayoutOptions.Start; // Kui dotnetBot on juba paremal, liigume vasakule
            }
        }
    }
}
