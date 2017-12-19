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
            client.Encoding = System.Text.Encoding.UTF8;

            string site = client.DownloadString("https://de.wikipedia.org/wiki/Eichen");
            string text = Text_filtern(site);
        }

        static string Text_filtern(string site)
        {
            site = " " + site; //Damit das Erste Zeichen niemals ein < Symbol ist.
            int start = 0; //0 gibt somit an, dass noch kein < Symbol vorgekommen ist.
            int startscript = 0; //Der startindex, wo <script> beginnt

            for (int i = 0; i < site.Length; i++)
            {
                if (site.Length - i > 7) //Am Ende kann der Substring nichtmehr ausgeführt werden.
                {
                    if (site.Substring(i, 8) == "<script>")
                        startscript = i;
                }

                if (startscript != 0) //Wenn startscript != 0 ist, wird </script> gesucht.
                {
                    if (site.Substring(i, 9) == "</script>")
                    {
                        site = site.Substring(0, startscript) + ' ' + site.Substring(i + 9);
                        i = i - (i - startscript) - 1;
                        startscript = 0;
                    }
                }
                else //Nur wenn nicht das </script > gesucht wird, soll nach <> und Sonderzeichen gesucht werden.
                {
                    char c = site[i];
                    switch (c)
                    {
                        //Alles was zwischen <> steht gehört zu HTML
                        case '<':
                            start = i;
                            break;
                        case '>':
                            site = site.Substring(0, start) + ' ' + site.Substring(i + 1);
                            i = i - (i - start) - 1;
                            start = 0;
                            break;
                    }

                    //Sonderzeichen rausfiltern
                    if (start == 0)
                    {
                        int ascii = site[i]; //Ist nich immer gleich c, da im Fall, dass c > der String verändert wird.

                        if ((ascii != 32 && ascii < 65) || (ascii > 90 && ascii < 97) || (ascii > 122 && ascii != 228 /*ä*/ && ascii != 246 /*ö*/ && ascii != 252 /*ü*/ && ascii != 196 /*Ä*/ && ascii != 214 /*Ö*/ && ascii != 220 /*Ü*/ && ascii != 223 /*ß*/)) //Alle Zeichen in diesen bereichen sind Sonderzeichen
                        {
                            site = site.Substring(0, i) + ' ' + site.Substring(i + 1);
                            i--;
                        }
                    }
                }
            }
            return site;
        }
    }
}