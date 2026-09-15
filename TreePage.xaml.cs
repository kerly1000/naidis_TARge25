using Microsoft.Maui.Layouts;
using System.Reflection.Metadata;

namespace naidis_TARge25;

public partial class TreePage : ContentPage

{
    public TreePage()
    {
        InitializeComponent();

        CreateTree();
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
            new Rect(0.5, 0.7, 60, 250)
        );

        AbsoluteLayout.SetLayoutFlags(
            trunk,
            AbsoluteLayoutFlags.PositionProportional
        );

        TreeLayout.Children.Add(trunk);

        //lehed
       
        AddLeaves(0.50, 0.40, 120, Colors.ForestGreen);
        AddLeaves(0.35, 0.48, 100, Colors.Green);
        AddLeaves(0.65, 0.48, 100, Colors.LimeGreen);
        AddLeaves(0.50, 0.30, 110, Colors.DarkGreen);
        AddLeaves(0.30, 0.40, 90, Colors.YellowGreen);
        AddLeaves(0.70, 0.40, 90, Colors.ForestGreen);
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
}
