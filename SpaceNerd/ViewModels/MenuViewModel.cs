using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace SpaceNerd.ViewModels;

public class MenuViewModel : ViewModelBase
{
    private MainViewModel _mainViewModel;
    
    public RelayCommand OpenApodCommand { get; }
    public RelayCommand OpenAsteroidsCommand { get; }
    public RelayCommand OpenMarsWeatherCommand { get; }
    public RelayCommand OpenSettingsCommand { get; }
    private static readonly string Username = SharedData.LoadFromLocal();
    private string _output = $"Hello, {Username}!";
    
    public MenuViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        OpenApodCommand = new RelayCommand(OpenApod);
        OpenAsteroidsCommand = new RelayCommand(OpenAsteroids);
        OpenMarsWeatherCommand = new RelayCommand(OpenMarsWeather);
        OpenSettingsCommand = new RelayCommand(OpenSettings);
        SharedData.OnUsernameChanged += UsernameChange;
    }
    
    public string Output
    {
        get => _output;
        set => this.RaiseAndSetIfChanged(ref _output, value);
    }

    private void UsernameChange(string newOutput)
    {
        Output = newOutput;
    }

    private void OpenApod()
    {
        var cachedViewModel = GetViewModel<ViewModelBase>("ApodViewModel");
        if (cachedViewModel != null)
        {
            _mainViewModel.CurrentView = cachedViewModel;
        }
        else
        {
            var apodViewModel = new ApodViewModel(_mainViewModel);
            _mainViewModel.CurrentView = apodViewModel;
            AddViewModel("ApodViewModel", apodViewModel);
        }
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
        var cachedViewModel = GetViewModel<ViewModelBase>("SettingsViewModel");
        if (cachedViewModel != null)
        {
            _mainViewModel.CurrentView = cachedViewModel;
        }
        else
        {
            var settingsViewModel = new SettingsViewModel(_mainViewModel);
            _mainViewModel.CurrentView = settingsViewModel;
            AddViewModel("SettingsViewModel", settingsViewModel);
        }
    }

    private static readonly Dictionary<string, object> ViewModelDictionary = new Dictionary<string, object>();

    private static void AddViewModel(string name, object viewmodel)
    {
        ViewModelDictionary[name] = viewmodel;
    }

    public static T GetViewModel<T>(string name) where T : ViewModelBase
    {
        if (ViewModelDictionary.TryGetValue(name, out var value))
        {
            return (value as T)!;
        }
        return null;
    }
}