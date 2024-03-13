using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpaceNerd.Models;

public class ApodService
{
    private const string ApiKey = "o6cG2thsukpfM4fD8DrcHMwceGFbdx7IBnJ2qJfo";
    private const string ApiUrl = "https://api.nasa.gov/planetary/apod";
    private string _time = "&date=" + DateTime.Today.ToString("yyyy-MM-dd");
    
    public async Task<ApodModel> GetApodAsync()
    {
        using HttpClient client = new HttpClient();
        string apiUrl = $"{ApiUrl}?api_key={ApiKey}{_time}&thumbs=true";
        try
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            
            string jsonResult = await response.Content.ReadAsStringAsync();
            
            return JsonConvert.DeserializeObject<ApodModel>(jsonResult)!;
        }
        catch (Exception)
        {
            Console.WriteLine("Не удалось связаться с сервером!");
            return null;
        }
    }
    
    public async Task<byte[]> DownloadImageAsync(string imageUrl)
    {
        using HttpClient client = new HttpClient();
        try 
        {
            HttpResponseMessage response = await client.GetAsync(imageUrl);

            byte[] image = await response.Content.ReadAsByteArrayAsync();
            string todayDate = DateTime.Today.ToString("dd-MM-yy");
            File.WriteAllBytes($"{todayDate}.png", image);
            
            return image;
        }
        catch (Exception)
        {
            Console.WriteLine($"Ошибка загрузки изображения");
            return null;
        }
    }
}