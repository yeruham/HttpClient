using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System;


public  class Client
{
    string url;
    HttpClient client = new HttpClient();
    public Client(string url)
    {
        this.url = url;
    }

    public class Post
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string bady { get; set; }
    }
    public async Task fetchPosts(int numPosts)
    {

        var response = await client.GetAsync(this.url);
        string answer = await response.Content.ReadAsStringAsync();
        Post[] posts = JsonSerializer.Deserialize<Post[]>(answer);

        for (int i = 0; i < numPosts; i++)
        {
            Post post = posts[i];
            Console.WriteLine(post.title);
        }
    }

}