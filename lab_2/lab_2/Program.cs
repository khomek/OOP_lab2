using System.Diagnostics;

SyncTask();
await AsyncTask();

async Task<string> asyncTask(string url){

    HttpClient client = new HttpClient();
    try
    {
        var res = await client.GetAsync(url);
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
void SyncTask(){
    Console.WriteLine("SyncTask");
    var time = new Stopwatch();
    time.Start();
    var first = asyncTask("https://official-joke-api.appspot.com/random_joke").Result;
    var second = asyncTask("https://api.exchangerate-api.com/v4/latest/USD").Result;
    var third = asyncTask("https://geek-jokes.sameerkumar.website/api?format=json").Result;

    Console.WriteLine($"URL1S is: \n{first}\n");
    Console.WriteLine($"URL2S is: \n {second}\n");
    Console.WriteLine($"URL3S is: \n {third}\n");

    time.Stop();
    Console.WriteLine($"Time for the sync program to work: {time.ElapsedMilliseconds} ms\n");
}
    
async Task AsyncTask(){
    Console.WriteLine("AyncTask");
    var time = new Stopwatch();
    time.Start();
    var first =   asyncTask("https://official-joke-api.appspot.com/random_joke");
    var second =  asyncTask("https://api.exchangerate-api.com/v4/latest/USD");
    var third =   asyncTask("https://geek-jokes.sameerkumar.website/api?format=json");

    await Task.WhenAll(first, second, third);

    Console.WriteLine($"URL1 is: \n{first.Result}\n");
    Console.WriteLine($"URL2 is: \n{second.Result}\n");
    Console.WriteLine($"URL3 is: \n{third.Result}\n");

    time.Stop();
    Console.WriteLine($"Time for the async program to work: {time.ElapsedMilliseconds} ms\n");
}