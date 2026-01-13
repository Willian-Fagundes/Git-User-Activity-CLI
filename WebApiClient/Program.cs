using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebApi;

using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

bool valid = true;

while(valid == true)
{
    
    Console.Write("github-activity (Type user name or 'exit' to quit)");

    string user = Console.ReadLine();

    if(string.IsNullOrEmpty(user) || user.ToLower() == "exit")
    {
        valid = false;
        continue;
    }

    try
    {
        string url = $"https://api.github.com/users/{user}/repos";

        var repositories = await ProcessRepositoriesAsync(client, url);

        if(repositories.Count == 0)
        {
            Console.WriteLine("No repositories found or user does not exist.");
        }

        foreach (var repo in repositories)
        {
            Console.WriteLine($"Name: {repo.Name}");
            Console.WriteLine($"Homepage: {repo.Homepage}");
            Console.WriteLine($"GitHub: {repo.GitHubHomeUrl}");
            Console.WriteLine($"Description: {repo.Description}");
            Console.WriteLine($"Watchers: {repo.Watchers:#,0}");
            Console.WriteLine($"Last Push: {repo.LastPush}");
            Console.WriteLine();
        }
    
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error fetching data: {ex.Message}");
    }
};

static async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client, string url)
        {
            var repositories = await client.GetFromJsonAsync<List<Repository>>(url);
            return repositories ?? new List<Repository>();
        }