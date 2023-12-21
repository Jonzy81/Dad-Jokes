using System;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace WebApplication1
{
    public class Program
    {
       
        public static async Task Main(string[] args)
        {
            
            using (HttpClient client = new HttpClient())
            {
                await Console.Out.WriteLineAsync("Typte enter for new joke or escape to exit");
                bool loop= true;
                while(loop)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Enter)
                    {
                        Console.Clear();
                        Joke newJoke = await GetRandomJokeAsync(client);
                        await Console.Out.WriteLineAsync(newJoke.joke);
                    }
                    if(key.Key==ConsoleKey.Escape)
                    {
                        break;
                    }
                }
            }
        }
        public static async Task<Joke> GetRandomJokeAsync(HttpClient client)
        {
            string url = "https://icanhazdadjoke.com";
            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, url))
            {
                req.Headers.Add("Accept", "application/json");
                HttpResponseMessage response = await client.SendAsync(req);
                string jokes = await response.Content.ReadAsStringAsync();
                Joke result = JsonSerializer.Deserialize<Joke>(jokes);

                return result;
            }
        } 
    }
    public class Joke
    {
        public string id { get; set; }
        public string joke { get; set; }
        public int status { get; set; }
    }
}
