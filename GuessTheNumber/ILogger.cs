﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessTheNumber
{
    internal interface ILogger
    {
        public void LogGameHistory(string difficulty, string name, int attempts, DateTime dateTime);
    }
}
