using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Wordlist
{
    class Program
    {
        static void Main(string[] args)
        {
            Website_durchsuchen();
        }

        static void Website_durchsuchen()
        {
            WebClient client = new WebClient();

            string site = Text_filtern(client.DownloadString("https://de.wikipedia.org/wiki/Eichen"));
        }

        static string Text_filtern(string site)
        {
            int start = 0;

            for (int i = 0; i < site.Length; i++)
            {
                char c = site[i];
                switch(c)
                {
                    case '<':
                        start = i;
                        break;
                    case '>':
                        site = site.Substring(0, start) + site.Substring(i + 1);
                        i = i - (i - start) - 1;
                        start = 0;
                        break;
                }
            }
            return site;
        }
    }
}