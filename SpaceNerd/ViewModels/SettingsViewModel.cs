using System;
using System.Reactive;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace SpaceNerd.ViewModels;

public class SettingsViewModel : ViewModelBase
{
    private readonly MainViewModel _mainViewModel;
    
    public RelayCommand BackToMenuCommand { get; }
    public RelayCommand OpenApodCommand { get; }
    public RelayCommand OpenAsteroidsCommand { get; }
    public RelayCommand OpenMarsWeatherCommand { get; }
    // public IEnumerable<FontFamily> FontFamilyNames { get; set; }
    private string _username;
    private string _output;
    
    public ReactiveCommand<Unit, Unit> SetNameCommand { get; }

    public SettingsViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        BackToMenuCommand = new RelayCommand(Back);
        OpenApodCommand = new RelayCommand(OpenApod);
        OpenAsteroidsCommand = new RelayCommand(OpenAsteroids);
        OpenMarsWeatherCommand = new RelayCommand(OpenMarsWeather);
        // FontFamilyNames = FontManager.Current
        //     .SystemFonts
        //     .Select(x => new FontFamily(x.Name));
        var isValidObservable = this.WhenAnyValue(x => x.Username, x => !string.IsNullOrWhiteSpace(x));
        SetNameCommand = ReactiveCommand.Create(CombineNameAndOutput, isValidObservable);
    }

    public string Username
    {
        get => _username;
        set
        {
            this.RaiseAndSetIfChanged(ref _username, value);
            SharedData.Output = Output;
        }
    }

    private string Output
    {
        get => _output;
        set
        {
            this.RaiseAndSetIfChanged(ref _output, value);
            SharedData.SaveToLocal(Username);
        }
    }

    private void CombineNameAndOutput()
    {
        Output = $"Hello, {_username}!";
        Username = String.Empty;
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

    private void OpenMarsWeather()
    {
        _mainViewModel.CurrentView = new MarsWeatherViewModel(_mainViewModel);
    }

}