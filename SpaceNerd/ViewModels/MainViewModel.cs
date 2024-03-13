using ReactiveUI;

namespace SpaceNerd.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private ViewModelBase _currentView;

    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => this.RaiseAndSetIfChanged(ref _currentView, value);
    }
    
    public MenuViewModel Menu { get; }

    public MainViewModel()
    {
        Menu = new MenuViewModel(this);
        CurrentView = Menu;
        SharedData.SaveToLocal("Newcomer");
    }
    
}