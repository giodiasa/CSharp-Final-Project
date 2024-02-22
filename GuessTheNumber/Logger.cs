using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    internal class Logger : ILogger
    {
        public void LogGameHistory(string difficulty, string name, int attempts, DateTime dateTime)
        {
            string header = "Difficulty,Name,Attempts,Time";
            string path = "C:\\Users\\giodi\\Desktop\\C# Final Project\\GuessTheNumber\\gamehistory.csv";
            string line = $"{difficulty},{name},{attempts},{dateTime}";
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
    }
}
