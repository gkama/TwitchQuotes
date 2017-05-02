using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchQuotes.Quotes
{
    class Popular
    {
        public List<TwitchQuote> TwitchQuotesList { get; set; }
        public List<string> TwitchQuotesStringList { get; set; }
        public string TwitchQuotesString { get; set; }
        public string URL { get; set; }

        //Constructor
        public Popular()
        {
            GetQuotes getQuotes = new GetQuotes(this.GetType().Name);
            this.TwitchQuotesList = getQuotes.TwitchQuotesList;
            this.TwitchQuotesStringList = getQuotes.TwitchQuotesStringList;
            this.TwitchQuotesString = getQuotes.TwitchQuotesString;
            this.URL = getQuotes.URL;
        }

        //Popular pages
        public Popular(int Pages)
        {
            if (Pages <= 10)
            {
                List<TwitchQuote> newTwitchQuotesList = new List<TwitchQuote>();
                List<string> newTwitchQuotesStringList = new List<string>();
                StringBuilder newTwitchQuotesString = new StringBuilder();

                //Loop through pages
                for (int i = 1; i <= Pages; i++)
                {
                    string URL = ConfigurationManager.AppSettings["PopularPage1"] + i + ConfigurationManager.AppSettings["PopularPage2"];
                    GetQuotes getQuotes = new GetQuotes("", URL);

                    foreach (TwitchQuote TQ in getQuotes.TwitchQuotesList)
                        newTwitchQuotesList.Add(TQ);
                    foreach (string s in getQuotes.TwitchQuotesStringList)
                        newTwitchQuotesStringList.Add(s);
                    newTwitchQuotesString.Append(getQuotes.TwitchQuotesString).Append("\r\n");
                }

                this.TwitchQuotesList = newTwitchQuotesList;
                this.TwitchQuotesStringList = newTwitchQuotesStringList;
                this.TwitchQuotesString = newTwitchQuotesString.ToString();
                this.URL = string.Empty;
            }
            else
            {
                this.TwitchQuotesList = null;
                this.TwitchQuotesStringList = null;
                this.TwitchQuotesString = string.Empty;
                this.URL = string.Empty;
            }
        }
    }
}
