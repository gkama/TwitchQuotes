using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchQuotes
{
    class TwitchQuote
    {
        public string Text { get; set; }
        public string Streamer { get; set; }
        public string Date { get; set; }
        public int VoteCount { get; set; }

        public TwitchQuote(string Text, string Streamer, string Date, int VoteCount)
        {
            this.Text = Text;
            this.Streamer = Streamer;
            this.Date = Date;
            this.VoteCount = VoteCount;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.Text, this.Streamer);
        }
    }
}
