using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    internal class Logger : ILogger
    {
        public void LogGameHistory(string difficulty, string name, double score, DateTime dateTime)
        {
            string header = "Difficulty,Name,Score,Time";
            string path = @"../../../gamehistory.csv";
            string line = $"{difficulty},{name},{score},{dateTime}";
            bool fileExists = File.Exists(path);
            using(StreamWriter sw = new StreamWriter(path, true))
            {
                if (!fileExists)
                {
                    sw.WriteLine(header);
                }
                sw.WriteLine(line);
            }
        }
        public void GetTop10()
        {
            string path = @"../../../gamehistory.csv";
            if (!File.Exists(path))
            {
                Console.WriteLine("No games have been played yet.");
                return;
            }
            var lines = File.ReadAllLines(path).Skip(1);
            var players = new List<GameResult>();
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                players.Add(new GameResult 
                { 
                    Name = parts[1],
                    Score = double.Parse(parts[2]),
                });
            }
            var top10Players = players.OrderByDescending(p => p.Score).Take(10).ToList();
            foreach (var player in top10Players)
            {
                Console.WriteLine($"{player.Name} - {player.Score}");
            }
        }
    }
}
