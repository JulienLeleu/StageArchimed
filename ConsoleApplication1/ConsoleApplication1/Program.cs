using System;
using System.IO;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Man m = new Man();
            string name = "Julien";
            Console.WriteLine(m);
            Console.WriteLine(m.sayHi(name));
            Console.WriteLine(name);*/

            //Requête Synchrone
           /* for (int i = 0; i < 10; i++)
            {
                WebRequest request = WebRequest.Create("http://api.allocine.fr/rest/v3/movie?partner=QXJjaGltZWQ&code=61282&format=json");
                WebResponse response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                Console.WriteLine(responseFromServer);
                reader.Close();
                response.Close();
            }*/

            //Requêtes Asynchrones
            /*WebRequestAsync requestAsync2 = new WebRequestAsync("https://www.googleapis.com/books/v1/volumes/-JthPwAACAAJ");
            requestAsync2.StartWebRequest();*/


            for (int i = 0; i < 300; i++)
            {
                //new WebRequestAsync("https://www.googleapis.com/books/v1/volumes/-JthPwAACAAJ").StartWebRequest();
                new WebRequestAsync(string.Format("http://api.allocine.fr/rest/v3/movie?partner=QXJjaGltZWQ&code={0}&format=json",6000+i)).StartWebRequest();
            }

            Console.WriteLine("\nCe message est censé s'afficher avant les reponses des requêtes asynchrones");
            Console.ReadKey();
        }
    }
}
