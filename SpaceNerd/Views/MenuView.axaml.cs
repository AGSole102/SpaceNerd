using Avalonia.Controls;
using Avalonia.Interactivity;

namespace SpaceNerd.Views;

public partial class MenuView : UserControl
{
    public MenuView()
    {
        InitializeComponent();
    }

    public void ShowInfo(object sender, RoutedEventArgs e)
    {
        InfoBlock.IsVisible = !InfoBlock.IsVisible;
    }
}