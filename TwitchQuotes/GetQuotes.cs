using System;
using System.Net;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace TwitchQuotes
{
    internal class GetQuotes
    {
        public List<TwitchQuote> TwitchQuotesList { get; set; }
        public List<string> TwitchQuotesStringList { get; set; }
        public string TwitchQuotesString { get; set; }
        public string URL { get; set; }

        //Constructor
        public GetQuotes(string Case, string URL = null)
        {
            try
            {
                this.TwitchQuotesList = new List<TwitchQuote>();
                this.TwitchQuotesStringList = new List<string>();
                if (URL == null)
                    this.URL = GetURL(Case);
                else
                    this.URL = URL;

                //Get the data
                GetData();

                //Set the TwitchQuotesStringList
                foreach (TwitchQuote TQ in this.TwitchQuotesList)
                    TwitchQuotesStringList.Add(TQ.Text);

                //Set the TwitchQuotesString
                this.TwitchQuotesString = GetQuotesString(Case);

            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Get the Data
        private void GetData()
        {
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(ReadData(this.URL));

                var quotes = doc.DocumentNode.SelectNodes(".//div[contains(@class, 'quote-list-card-parent')]");
                foreach (var li in quotes)
                {
                    if (li.SelectSingleNode(".//div[contains(@class, 'quote_text_multi_line')]") != null)
                    {
                        string Text = string.Empty;
                        int VoteCount = 0;
                        string Streamer = string.Empty;
                        string Date = string.Empty;

                        Text = li.SelectSingleNode(".//div[contains(@class, 'quote_text_multi_line')]").SelectNodes(".//span[contains(@id, 'quote_display_content')]")[0].InnerText.Trim();
                        VoteCount = int.Parse(li.SelectSingleNode(".//div[@class='ip_vote_count']").InnerText.Trim());
                        if (li.SelectSingleNode(".//div[@class='-stream-name-parent']") != null)
                            Streamer = li.SelectSingleNode(".//div[@class='-stream-name-parent']").SelectSingleNode(".//a").InnerText.Trim();
                        Date = li.SelectSingleNode(".//span[@class='-date']").InnerText.Trim();

                        //Add quotes
                        this.TwitchQuotesList.Add(new TwitchQuote(Text, Streamer, Date, VoteCount));
                    }
                }
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }

        //Read the data from URL
        private string ReadData(string URL)
        {
            try
            {
                WebRequest request = WebRequest.Create(URL);
                WebResponse response = request.GetResponse();
                Stream data = response.GetResponseStream();
                string html = String.Empty;
                using (StreamReader sr = new StreamReader(data))
                {
                    html = sr.ReadToEnd();
                }
                return html;
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }


        #region Parsing Quotes
        //Get the quotes
        private string GetQuotesString(string Case)
        {
            try
            {
                StringBuilder toReturn = new StringBuilder();
                foreach (TwitchQuote TQ in this.TwitchQuotesList)
                    toReturn.Append(TQ.ToString()).Append("\r\n");
                return toReturn.ToString().TrimEnd('\r').TrimEnd('\n');
            }
            catch (Exception e) { throw new Exception(e.Message); }
        }
        private string GetURL(string Case)
        {
            switch (Case)
            {
                case "Popular":
                    return ConfigurationManager.AppSettings["Popular"];
                case "Hearthstone":
                    return ConfigurationManager.AppSettings["Hearthstone"];
                case "LeagueOfLegends":
                    return ConfigurationManager.AppSettings["LeagueOfLegends"];
                case "Salty":
                    return ConfigurationManager.AppSettings["Salty"];
                case "PlebsVsSubs":
                    return ConfigurationManager.AppSettings["PlebsVsSubs"];
                case "EUvsNA":
                    return ConfigurationManager.AppSettings["EUvsNA"];
                case "Sellout":
                    return ConfigurationManager.AppSettings["Sellout"];
                case "VapeNation":
                    return ConfigurationManager.AppSettings["VapeNation"];
                case "Latest":
                    return ConfigurationManager.AppSettings["Latest"];
                case "Oldest":
                    return ConfigurationManager.AppSettings["Oldest"];
                default:
                    return "";
            }
        }
        #endregion
    }
}
