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

    
    private AbsoluteLayout treeContainer;
    private BoxView trunk;

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
            AbsoluteLayoutFlags.All
        );

        TreeLayout.Children.Add(grass);


        // Puu konteiner
        treeContainer = new AbsoluteLayout();

        treeContainer.AnchorX = 0.5;
        treeContainer.AnchorY = 0.8;

        AbsoluteLayout.SetLayoutBounds(
            treeContainer,
            new Rect(0, 0, 1, 1)
        );

        AbsoluteLayout.SetLayoutFlags(
            treeContainer,
            AbsoluteLayoutFlags.All
        );

        TreeLayout.Children.Add(treeContainer);


        // Tüvi
        trunk = new BoxView
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

        treeContainer.Children.Add(trunk);


        // Lehed
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

        treeContainer.Children.Add(leaf);

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

        treeContainer.Children.Add(flower);

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
        actionPicker.Items.Add("Sahise");
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

        else if (action == "Sahise")
        {
            await ShakeTree();
        }

        else if (action == "Langeta")
        {
            int month = datePicker.Date.Value.Month;

            TimeSpan selectedTime = timePicker.Time ?? TimeSpan.Zero;
            int hour = selectedTime.Hours;

            bool isWinter = month == 12 || month == 1 || month == 2;
            bool isDaytime = hour >= 8 && hour < 17;

            if (isWinter && isDaytime)
            {
                await CutDownTree();
            }
            else
            {
                infoLabel.Text = "Puid võib langetada ainult talvel ja kell 08:00–17:00.";
            }
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
        // õite loomine, kui neid ei ole
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

        
        // kontrollime, kas õied on nähtavad
        bool flowersVisible = flowers[0].Opacity > 0;

        if (!flowersVisible)
        {
            infoLabel.Text = "Puu õitseb!";

            List<Task> animations = new List<Task>();

            foreach (Frame flower in flowers)
            {
                animations.Add(
                    flower.FadeTo(1, animationSpeed)
                );
            }

            await Task.WhenAll(animations);
        }
        
        else
        {
            infoLabel.Text = "Enam ei õitse";

            List<Task> animations = new List<Task>();

            foreach (Frame flower in flowers)
            {
                animations.Add(
                    flower.FadeTo(0, animationSpeed)
                );
            }

            await Task.WhenAll(animations);

            infoLabel.Text = "Puu ei õitse";
        }
    }

    //meetod sahistamiseks 
    private async Task ShakeTree()
    {
        infoLabel.Text = "Puu sahiseb tuules...";

        List<Task> animations = new List<Task>();

        foreach (Frame leaf in leaves)
        {
            animations.Add(ShakeLeaf(leaf));
        }

        await Task.WhenAll(animations);

        infoLabel.Text = "Puu ei sahise";
    }

    //meetod lehe liigutamiseks
    private async Task ShakeLeaf(Frame leaf)
    {
        await leaf.TranslateTo(-10, 0, animationSpeed / 4);
        await leaf.TranslateTo(10, 0, animationSpeed / 2);
        await leaf.TranslateTo(-10, 0, animationSpeed / 2);
        await leaf.TranslateTo(0, 0, animationSpeed / 4);
    }

    //meetod puu langetamiseks
    private async Task CutDownTree()
    {
        infoLabel.Text = "Ettevaatust! Puu langeb...";

        await treeContainer.RotateTo(
            90,
            animationSpeed
        );

        infoLabel.Text = "Langetatud";
    }
}
