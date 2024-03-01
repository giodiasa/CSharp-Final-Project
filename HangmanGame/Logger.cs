using System.Xml.Linq;

namespace HangmanGame
{
    internal class Logger : ILogger
    {
        public void LogGameHistory(string name, int score, string dateTime, string word, string result, string difficulty)
        {
            string path = @"../../../gamehistory.xml";
            XDocument xmlDoc;
            if (File.Exists(path))
            {
                xmlDoc = XDocument.Load(path);
            }
            else
            {
                xmlDoc = new XDocument(new XElement("Games"));
            }

            XElement gameElement = new XElement("Game",
                new XElement("PlayerName", name),
                new XElement("Score", score),
                new XElement("PlayDate", dateTime),
                new XElement("Word", word),
                new XElement("Result", result),
                new XElement("Difficulty", difficulty));

            xmlDoc.Root?.Add(gameElement);

            xmlDoc.Save(path);

            Console.WriteLine("Game data logged successfully!");
        }
    }
}
