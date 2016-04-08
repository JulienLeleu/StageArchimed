using ConsoleApplication1;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

public class Author : System.Attribute
{
    string name;
    public double version;

    public Author(string name)
    {
        this.name = name;
        version = 1.0;
    }

    public string GetName()
    {
        return name;
    }
}

[Author("Julien LELEU", version = 1.0)]
public class WebRequestAsync
{
    private HttpWebRequest webRequest;

    public WebRequestAsync(string url)
	{
        webRequest = (HttpWebRequest)WebRequest.Create(url);
	}

    public void StartWebRequest()
    {
        try
        {
            webRequest.BeginGetResponse(new AsyncCallback(DisplayWebResponse), webRequest);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private void DisplayWebResponse(IAsyncResult result)
    {
        Stream dataStream = null;
        //StreamReader reader = null;
        HttpWebResponse response = null;
        try
        {
            response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            dataStream = response.GetResponseStream();
            //Book b = DeSerializeToJson<Book>(dataStream);
            MovieFromAllocine m = DeSerializeToJson<MovieFromAllocine>(dataStream);
            /*reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();*/
            Console.WriteLine(m.Movie.Title);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            //reader.Close();
            response.Close();
        }
    }

    public static T DeSerializeToJson<T>(Stream stream)
    {
        using (stream)
        {
            var deserializer = new DataContractJsonSerializer(typeof(T));
            return (T)deserializer.ReadObject(stream);
        }
    }
}
