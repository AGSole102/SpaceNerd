using System;
using CommunityToolkit.Mvvm.Input;

namespace SpaceNerd.ViewModels;

public class ApodViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    
    public RelayCommand BackToMenuCommand { get; }
    public RelayCommand OpenAsteroidsCommand { get; }
    public RelayCommand OpenMarsWeatherCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    public string TodayDate { get; set; }

    public ApodViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        BackToMenuCommand = new RelayCommand(Back);
        OpenAsteroidsCommand = new RelayCommand(OpenAsteroids);
        OpenMarsWeatherCommand = new RelayCommand(OpenMarsWeather);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
        TodayDate = DateTime.Today.ToShortDateString();
    }

    private void Back()
    {
        _mainViewModel.CurrentView = _mainViewModel.Menu;
    }

    private void OpenAsteroids()
    {
        _mainViewModel.CurrentView = new AsteroidsViewModel(_mainViewModel);
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
