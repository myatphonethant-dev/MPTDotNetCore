using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Newtonsoft.Json;
using MPTDotNetCore.Shared.Models;

namespace MPTDotNetCore.ConsoleAppHttpClientExamples;

internal class HttpClientExample
{
    private readonly HttpClient _client = new HttpClient() { BaseAddress = new Uri("https://localhost:7224") };
    private readonly string _blogEndpoint = "api/blog";

    public async Task RunAsync()
    {
        //await ReadAsync();
        await EditAsync(1087);
        //await CreateAsync("qwe", "qwe", "qwe");
        await UpdateAsync(1087, "qwe", "qwe", "qwe");
    }

    private async Task ReadAsync()
    {
        var response = await _client.GetAsync(_blogEndpoint);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            var lst = JsonConvert.DeserializeObject<List<BlogModel>>(jsonStr)!;

            foreach (var item in lst)
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
                Console.WriteLine($"Title => {item.BlogTitle}");
                Console.WriteLine($"Author => {item.BlogAuthor}");
                Console.WriteLine($"Content => {item.BlogContent}");
            }
        }
    }

    private async Task EditAsync(int id)
    {
        var response = await _client.GetAsync($"{_blogEndpoint}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string jsonStr = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<BlogModel>(jsonStr)!;
            Console.WriteLine(JsonConvert.SerializeObject(item));
            Console.WriteLine($"Title => {item.BlogTitle}");
            Console.WriteLine($"Author => {item.BlogAuthor}");
            Console.WriteLine($"Content => {item.BlogContent}");
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }

    private async Task CreateAsync(string title, string author, string content)
    {
        BlogModel BlogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string blogJson = JsonConvert.SerializeObject(BlogModel);

        var httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
        var response = await _client.PostAsync(_blogEndpoint, httpContent);

        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }

    private async Task UpdateAsync(int id, string title, string author, string content)
    {
        var BlogModel = new BlogModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        string blogJson = JsonConvert.SerializeObject(BlogModel);

        var httpContent = new StringContent(blogJson, Encoding.UTF8, Application.Json);
        var response = await _client.PutAsync($"{_blogEndpoint}/{id}", httpContent);

        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }

    private async Task DeleteAsync(int id)
    {
        var response = await _client.DeleteAsync($"{_blogEndpoint}/{id}");

        if (response.IsSuccessStatusCode)
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
        else
        {
            string message = await response.Content.ReadAsStringAsync();
            Console.WriteLine(message);
        }
    }
}