using Microsoft.Maui.Layouts;

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
    public TreePage()
    {
        InitializeComponent();

        CreateTree();
        CreateControlPanel();
    }


    private void CreateTree()
    {
        // tüvi
        BoxView trunk = new BoxView
        {
            Color = Colors.SaddleBrown
        };

        AbsoluteLayout.SetLayoutBounds(
            trunk,
            new Rect(0.5, 0.65, 50, 150)
        );

        AbsoluteLayout.SetLayoutFlags(
            trunk,
            AbsoluteLayoutFlags.PositionProportional
        );

        TreeLayout.Children.Add(trunk);


        // lehed
        AddLeaves(0.50, 0.30, 90, Colors.ForestGreen);
        AddLeaves(0.35, 0.40, 75, Colors.Green);
        AddLeaves(0.65, 0.40, 75, Colors.LimeGreen);
        AddLeaves(0.50, 0.20, 80, Colors.DarkGreen);
        AddLeaves(0.35, 0.30, 65, Colors.YellowGreen);
        AddLeaves(0.65, 0.30, 65, Colors.ForestGreen);
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


        // Stepper
        speedStepper = new Stepper
        {
            Minimum = 500,
            Maximum = 2000,
            Increment = 100,
            Value = 1000
        };

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


        // Juhtpaneel ScrollView sisse
        ScrollView controlScroll = new ScrollView
        {
            Content = ControlPanel
        };


        
    }
}
