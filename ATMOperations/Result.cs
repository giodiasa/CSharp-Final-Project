﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMOperations
{
    internal class Result
    {
        public string PersonalNumber { get; set; } = string.Empty;
        public string Event { get; set; } = string.Empty;
        public override string ToString()
        {
            return Event;
        }
    }
}
