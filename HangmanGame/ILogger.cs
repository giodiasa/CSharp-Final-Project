using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame
{
    internal interface ILogger
    {
        public void LogGameHistory(string name, int score, string dateTime, string word, string result);
    }
}
