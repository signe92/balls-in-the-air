using System;
using System.IO;

namespace BoldeILuften;

public class HighscoreStore
{
    private readonly string _filePath;

    public HighscoreStore()
    {
        string folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "BallsInTheAir");

            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, "highscore.txt");
    }

    public int Load() // henter highscore
    {
        if (!File.Exists(_filePath)) // hvis filen ikke findes
        return 0;

        string text = File.ReadAllText(_filePath); // laver teksten om til tal

        if (int.TryParse(text, out int score))
        return score;

        return 0; // starter forfra
    }

    public void Save(int score)
    {
        File.WriteAllText(_filePath, score.ToString());
    }
}