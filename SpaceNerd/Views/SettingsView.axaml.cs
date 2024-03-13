using Avalonia.Controls;

namespace SpaceNerd.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
        // ComboBox combobox = this.Find<ComboBox>("FontFamilySetting");
        // combobox.ItemsSource = FontManager.Current
        //     .SystemFonts
        //     .OrderBy(x => x)
        //     .Select(x => new FontFamily(x.Name));
        // combobox.SelectedIndex = 0;
    }
}