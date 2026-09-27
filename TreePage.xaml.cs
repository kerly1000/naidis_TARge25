using Microsoft.Maui.Layouts;
using Microsoft.Maui.Controls;

namespace naidis_TARge25;

public partial class TreePage : ContentPage

{
    private Picker actionPicker;
    private Button startButton;
    private Label infoLabel;
    private Slider opacitySlider;
    private Stepper speedStepper;
    private Label speedLabel;
    private DatePicker datePicker;
    private TimePicker timePicker;

    private List<Frame> leaves = new List<Frame>();
    private List<Frame> flowers = new List<Frame>();
    private uint animationSpeed = 1000;
    
    public TreePage()
    {
        InitializeComponent();

        CreateTree();
        CreateControlPanel();
    }

    // Meetod puu loomiseks
    private void CreateTree()
    {
        // Muru
        // Muru
        BoxView grass = new BoxView
        {
            Color = Colors.ForestGreen
        };

        AbsoluteLayout.SetLayoutBounds(
            grass,
            new Rect(0, 1, 1, 0.27)
        );

        AbsoluteLayout.SetLayoutFlags(
            grass,
            AbsoluteLayoutFlags.XProportional |
            AbsoluteLayoutFlags.YProportional |
            AbsoluteLayoutFlags.WidthProportional |
            AbsoluteLayoutFlags.HeightProportional
        );

        TreeLayout.Children.Add(grass);

        // tüvi
        BoxView trunk = new BoxView
        {
            Color = Colors.SaddleBrown
        };

        AbsoluteLayout.SetLayoutBounds(
            trunk,
            new Rect(0.5, 0.68, 50, 150)
        );

        AbsoluteLayout.SetLayoutFlags(
            trunk,
            AbsoluteLayoutFlags.PositionProportional
        );

        TreeLayout.Children.Add(trunk);


        // lehed
        AddLeaves(0.50, 0.35, 90, Colors.ForestGreen);
        AddLeaves(0.35, 0.45, 75, Colors.Green);
        AddLeaves(0.65, 0.45, 75, Colors.LimeGreen);
        AddLeaves(0.50, 0.25, 80, Colors.DarkGreen);
        AddLeaves(0.35, 0.35, 65, Colors.YellowGreen);
        AddLeaves(0.65, 0.35, 65, Colors.ForestGreen);
    }


    // Meetod ühe lehe lisamiseks
    private void AddLeaves(
        double x,
        double y,
        double size,
        Color color)
    {
        Frame leaf = new Frame
        {
            BackgroundColor = color,
            CornerRadius = (float)(size / 2),
            WidthRequest = size,
            HeightRequest = size,
            Padding = 0,
            HasShadow = false
        };

        AbsoluteLayout.SetLayoutBounds(
            leaf,
            new Rect(x, y, size, size)
        );

        AbsoluteLayout.SetLayoutFlags(
            leaf,
            AbsoluteLayoutFlags.PositionProportional
        );

        TreeLayout.Children.Add(leaf);

        leaves.Add(leaf);
    }

    //Meetod õite lisamiseks
    private void AddFlower(double x, double y, double size)
    {
        Frame flower = new Frame
        {
            BackgroundColor = Colors.HotPink,
            CornerRadius = (float)(size / 2),
            WidthRequest = size,
            HeightRequest = size,
            Padding = 0,
            HasShadow = false,
            Opacity = 0
        };

        AbsoluteLayout.SetLayoutBounds(
            flower,
            new Rect(x, y, size, size)
        );

        AbsoluteLayout.SetLayoutFlags(
            flower,
            AbsoluteLayoutFlags.PositionProportional
        );

        TreeLayout.Children.Add(flower);

        flowers.Add(flower);
    }
    private void CreateControlPanel()
    {
        // Picker
        actionPicker = new Picker
        {
            Title = "Vali tegevus"
        };

        actionPicker.Items.Add("Kasva");
        actionPicker.Items.Add("Õitse");
        actionPicker.Items.Add("Värise");
        actionPicker.Items.Add("Langeta");


        // Button
        startButton = new Button
        {
            Text = "Käivita"
        };

        startButton.Clicked += OnStartClicked;


        // Label
        infoLabel = new Label
        {
            Text = "Vali tegevus",
            HorizontalOptions = LayoutOptions.Center
        };


        // Slider
        opacitySlider = new Slider
        {
            Minimum = 0.0,
            Maximum = 1.0,
            Value = 1.0
        };

        opacitySlider.ValueChanged += OnOpacityChanged;

        // Stepper
        speedStepper = new Stepper
        {
            Minimum = 500,
            Maximum = 2000,
            Increment = 100,
            Value = 1000
        };

        speedStepper.ValueChanged += OnSpeedChanged;

        speedLabel = new Label
        {
            Text = "Kiirus: 1000 ms",
            HorizontalOptions = LayoutOptions.Center
        };


        // DatePicker
        datePicker = new DatePicker
        {
            Format = "dd.MM.yyyy"
        };


        // TimePicker
        timePicker = new TimePicker
        {
            Format = "HH:mm"
        };


        ControlPanel.Children.Add(actionPicker);
        ControlPanel.Children.Add(startButton);
        ControlPanel.Children.Add(infoLabel);

        ControlPanel.Children.Add(
            new Label
            {
                Text = "Lehestiku läbipaistvus"
            }
        );

        ControlPanel.Children.Add(opacitySlider);

        ControlPanel.Children.Add(
            new Label
            {
                Text = "Animatsiooni kiirus"
            }
        );

        ControlPanel.Children.Add(speedStepper);
        ControlPanel.Children.Add(speedLabel);

        ControlPanel.Children.Add(
            new Label
            {
                Text = "Aastaaeg"
            }
        );

        ControlPanel.Children.Add(datePicker);

        ControlPanel.Children.Add(
            new Label
            {
                Text = "Kellaaeg"
            }
        );

        ControlPanel.Children.Add(timePicker);

       
    }
    //slideri meetod, mis muudab lehtede läbipaistvust
    private void OnOpacityChanged(object sender, ValueChangedEventArgs e)
    {
        foreach (Frame leaf in leaves)
        {
            leaf.Opacity = e.NewValue;
        }
    }

    //stepperi meetod, mis muudab animatsiooni kiirust
    private void OnSpeedChanged(object sender, ValueChangedEventArgs e)
    {
        animationSpeed = (uint)e.NewValue;

        speedLabel.Text = $"Kiirus: {animationSpeed} ms";
    }

    //pickeri meetod, mis käivitab valitud tegevuse
    private async void OnStartClicked(object sender, EventArgs e)
    {
        if (actionPicker.SelectedIndex == -1)
        {
            infoLabel.Text = "Palun vali tegevus!";
            return;
        }

        string action = actionPicker.SelectedItem.ToString();

        if (action == "Kasva")
        {
            await GrowTree();
        }

        else if (action == "Õitse")
        {
            await BloomTree();
        }
    }

    //puu kasvamise meetod
    private async Task GrowTree()
    {
        infoLabel.Text = "Puu kasvab...";

        List<Task> animations = new List<Task>();

        foreach (Frame leaf in leaves)
        {
            animations.Add(
                leaf.ScaleTo(
                    leaf.Scale * 1.2,
                    animationSpeed
                )
            );
        }

        await Task.WhenAll(animations);

        infoLabel.Text = "Puu kasvas!";
    }

    //õite kasvamise meetod
    private async Task BloomTree()
    {
        infoLabel.Text = "Puu õitseb...";

        if (flowers.Count == 0)
        {
            AddFlower(0.45, 0.30, 20);
            AddFlower(0.55, 0.28, 18);
            AddFlower(0.38, 0.38, 16);
            AddFlower(0.62, 0.38, 20);
            AddFlower(0.50, 0.20, 18);
            AddFlower(0.32, 0.30, 15);
            AddFlower(0.68, 0.30, 15);
        }

        List<Task> animations = new List<Task>();

        foreach (Frame flower in flowers)
        {
            animations.Add(
                flower.FadeTo(1, animationSpeed)
            );
        }

        await Task.WhenAll(animations);

        infoLabel.Text = "Puu õitseb!";
    }
}
