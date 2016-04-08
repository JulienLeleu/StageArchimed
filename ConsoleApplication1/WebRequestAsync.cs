using System;

public class WebRequestAsync
{
    private HttpWebRequest webRequest;

    public WebRequestAsync(string url)
	{
        webRequest = webRequest.Create(url);
	}

    void StartWebRequest()
    {
        try
        {
            webRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), webRequest);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }
    }

    void FinishWebRequest(IAsyncResult result)
    {
        try
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            reader.Close();
            response.Close();
        }
    }
}
