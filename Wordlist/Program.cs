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

        }

        string Website_durchsuchen()
        {
            WebClient client = new WebClient();

            string st = client.DownloadString(https://de.wikipedia.org/wiki/Eichen);
        }


    }
}