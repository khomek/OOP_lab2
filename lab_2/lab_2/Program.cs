using System.Diagnostics;


    SyncTask();
    AsyncTask().Wait(3000);

async Task<string> asyncTask(string url){
    HttpClient client = new HttpClient();
    try
    {
        var res = client.GetAsync(url).Result;
        if (res.IsSuccessStatusCode)
        {
            return await res.Content.ReadAsStringAsync();
        }
        else
        {
            Console.WriteLine($"request error: {res.StatusCode}");
            return null;
        }
    }
    catch (HttpRequestException ex)
    {
        Console.WriteLine($"Somthing went wrong with your url {url}: {ex.Message}");
        return null;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Somthing went wrong {ex.Message}");
        return null;
    }
}
void SyncTask()
{
    Console.WriteLine("SyncTask");
    var time = new Stopwatch();
    time.Start();
    var first = asyncTask("https://official-joke-api.appspot.com/random_joke").Result;
    Console.WriteLine($"URL1 is: \n{first}\n");
    var second = asyncTask("https://api.exchangerate-api.com/v4/latest/USD").Result;
    Console.WriteLine($"URL2 is: \n {second}\n");
    var third = asyncTask("https://geek-jokes.sameerkumar.website/api?format=json").Result;
    Console.WriteLine($"URL3 is: \n {third}\n");
    time.Stop();
    Console.WriteLine($"Time for the sync program to work: {time.ElapsedMilliseconds} ms\n");
}
async Task<string> AsyncTask()
{
    Console.WriteLine("AyncTask");
    var time = new Stopwatch();
    time.Start();
    var first1 = await asyncTask("https://official-joke-api.appspot.com/random_joke");
    Console.WriteLine($"URL1 is: \n{first1}\n");
    var second1 = await asyncTask("https://api.exchangerate-api.com/v4/latest/USD");
    Console.WriteLine($"URL2 is: \n{second1}\n");
    var third1 = await asyncTask("https://geek-jokes.sameerkumar.website/api?format=json");
    Console.WriteLine($"URL3 is: \n{third1}\n");
    time.Stop();
    Console.WriteLine($"Time for the async program to work: {time.ElapsedMilliseconds} ms\n");
    return null;
}