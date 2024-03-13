using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using SpaceNerd.Models;
using SpaceNerd.ViewModels;

namespace SpaceNerd.Views;

public partial class ApodView : UserControl
{
    public ApodView()
    {
        InitializeComponent();

        Dates = Enumerable.Range(0, 7)
            .Select(offset => DateTime.Today.AddDays(-offset))
            .ToList();
        MyDateTimes = Dates.Select(d => d.ToShortDateString()).ToList();

        if (MenuViewModel.GetViewModel<ViewModelBase>("ApodViewModel") == null)
        {
            InitializeAsync();
        }

        // ComboBox comboBox = this.Find<ComboBox>("Date")!;
        // comboBox.ItemsSource = MyDateTimes;
        // comboBox.SelectedIndex = 0;
        Image.Source = GetImage;
        Title.Text = GetTitle;
        MainText.Text = GetExplanation;
    }
    
    public List<string> MyDateTimes { get; set; }
    private List<DateTime> Dates { get; set; }
    
    private ApodModel _result;
    private static Bitmap GetImage { get; set; }
    private static string GetTitle { get; set; }
    private static string GetExplanation { get; set; }
    private static string GetDate { get; set; }
    
    private async void InitializeAsync()
    {
        var service = new ApodService();
        _result = await service.GetApodAsync();
        string todayDate = DateTime.Today.ToString("dd-MM-yy");
        
        if (File.Exists($"{todayDate}.png"))
        {
            GetImage = new Bitmap(new MemoryStream(await File.ReadAllBytesAsync($"{todayDate}.png")));
            Image.Source = GetImage;
        }
        else
        {
            byte[] imageByte;
            if (_result.Media_Type == "image")
            {
                imageByte = await service.DownloadImageAsync(_result.HDURL);
            }
            else
            {
                imageByte = await service.DownloadImageAsync(_result.Thumbnail_URL);
            }
            GetImage = new Bitmap(new MemoryStream(imageByte));
            Image.Source = GetImage;
        }

        if (GetDate == null || GetTitle == null || GetExplanation == null)
        {
            GetTitle = _result.Title;
            GetExplanation = _result.Explanation;
            string[] tmp = _result.Date.Split("-");
            GetDate += tmp[2] + " - " + tmp[1] + " - " + tmp[0];
            Title.Text = GetTitle;
            MainText.Text = GetExplanation;
        }
    }

    private void ShowInfo(object sender, RoutedEventArgs e)
    {
        InfoBlock.IsVisible = !InfoBlock.IsVisible;
    }
    
}