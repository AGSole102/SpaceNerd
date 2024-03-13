using CommunityToolkit.Mvvm.Input;

namespace SpaceNerd.ViewModels;

public class AsteroidsViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    
    public RelayCommand BackToMenuCommand { get; }
    public RelayCommand OpenApodCommand { get; }
    public RelayCommand OpenMarsWeatherCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    

    public AsteroidsViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        BackToMenuCommand = new RelayCommand(Back);
        OpenApodCommand = new RelayCommand(OpenApod);
        OpenMarsWeatherCommand = new RelayCommand(OpenMarsWeather);
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

    private void OpenMarsWeather()
    {
        _mainViewModel.CurrentView = new MarsWeatherViewModel(_mainViewModel);
    }
    
    private void OpenSettings()
    {
        _mainViewModel.CurrentView = new SettingsViewModel(_mainViewModel);
    }
}