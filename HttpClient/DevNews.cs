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
        public string body { get; set; }
    }
    public async Task fetchPosts(int numPosts)
    {

        var response = await client.GetAsync(this.url);
        //Console.WriteLine("response: " + response);
        string answer = await response.Content.ReadAsStringAsync();
        //Console.WriteLine("answer: " + answer);
        Post[] posts = JsonSerializer.Deserialize<Post[]>(answer);

        for (int i = 0; i < numPosts && i < posts.Length; i++)
        {
            Post post = posts[i];
            Console.WriteLine(post.title);
        }
    }

    public async Task postById(int id)
    {
        string newUrl = this.url + "?id=" + id;
        var response = await client.GetAsync(newUrl);
        string text = await response.Content.ReadAsStringAsync();
        Post[] posts = JsonSerializer.Deserialize<Post[]>(text);
        if (posts.Length > 0)
        {
            foreach (Post post in posts)
            {
                Console.WriteLine($"Post ID: {post.id}\nTitle: {post.title}\nbady: {post.body}");
            }
        }
        else
        {
            Console.WriteLine("The post number does not exist.");
        }
    }

}