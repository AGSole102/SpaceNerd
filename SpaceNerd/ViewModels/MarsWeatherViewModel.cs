using CommunityToolkit.Mvvm.Input;

namespace SpaceNerd.ViewModels;

public class MarsWeatherViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    
    public RelayCommand BackToMenuCommand { get; }
    public RelayCommand OpenApodCommand { get; }
    public RelayCommand OpenAsteroidsCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }

    public MarsWeatherViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        BackToMenuCommand = new RelayCommand(Back);
        OpenApodCommand = new RelayCommand(OpenApod);
        OpenAsteroidsCommand = new RelayCommand(OpenAsteroids);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
    }

    private void Back()
    {
        _mainViewModel.CurrentView = _mainViewModel.Menu;
    }
    
    private void OpenApod()
    {
        _mainViewModel.CurrentView = new ApodViewModel(_mainViewModel);
    }

    private void OpenAsteroids()
    {
        _mainViewModel.CurrentView = new AsteroidsViewModel(_mainViewModel);
    }

    private void OpenSettings()
    {
        _mainViewModel.CurrentView = new SettingsViewModel(_mainViewModel);
    }
}