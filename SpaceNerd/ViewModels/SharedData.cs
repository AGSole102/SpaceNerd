using System;
using System.IO;

namespace SpaceNerd.ViewModels;

public static class SharedData
{
    private static string _output;

    public static string Output
    {
        get => _output;
        set
        {
            if (_output != value)
            {
                _output = value;
                OnUsernameChanged?.Invoke(_output);
            }
        }
    }

    public static event Action<string> OnUsernameChanged;

    private static readonly string filePath = "username.txt";

    public static void SaveToLocal(string data)
    {
        File.WriteAllText(filePath, data);
    }

    public static string LoadFromLocal()
    {
        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            return String.Empty;
        }
    }
}