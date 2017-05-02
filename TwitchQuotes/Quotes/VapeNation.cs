using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchQuotes.Quotes
{
    class VapeNation
    {
        public List<TwitchQuote> TwitchQuotesList { get; set; }
        public List<string> TwitchQuotesStringList { get; set; }
        public string TwitchQuotesString { get; set; }
        public string URL { get; set; }

        //Constructor
        public VapeNation()
        {
            GetQuotes getQuotes = new GetQuotes(this.GetType().Name);
            this.TwitchQuotesList = getQuotes.TwitchQuotesList;
            this.TwitchQuotesStringList = getQuotes.TwitchQuotesStringList;
            this.TwitchQuotesString = getQuotes.TwitchQuotesString;
            this.URL = getQuotes.URL;
        }
    }
}
