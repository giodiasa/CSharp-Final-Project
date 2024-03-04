using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using System.Xml.Linq;

namespace GuessTheNumber
{
    internal class GameResult
    {
        public string Name { get; set; } = string.Empty;
        public double Score { get; set; }
    }
}
