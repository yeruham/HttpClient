using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text;
using System;
using System.Collections.Generic;


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

    //exercise one:
    public async Task fetchPosts(int numPosts)
    {

        var response = await client.GetAsync(this.url);
        Console.WriteLine("response: " + response.Content.GetType());
        string answer = await response.Content.ReadAsStringAsync();
        //Console.WriteLine("answer: " + answer);
        Post[] posts = JsonSerializer.Deserialize<Post[]>(answer);

        for (int i = 0; i < numPosts && i < posts.Length; i++)
        {
            Post post = posts[i];
            Console.WriteLine(post.title);
        }
    }

    //exercise two:
    public async Task getPostById(int id)
    {
        string newUrl = this.url + "?id=" + id;
        //var response = await client.GetAsync(newUrl);
        //string text = await response.Content.ReadAsStringAsync();
        string text = await client.GetStringAsync(newUrl);
        Console.WriteLine(text);
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

    //exercise three:
    public async Task PostNew(int userId, string title, string body)
    {
        Post newPost = new Post();
        newPost.userId = userId;
        newPost.title = title;
        newPost.body = body;

        string text = JsonSerializer.Serialize(newPost);
        
        var content = new StringContent(text, Encoding.UTF8, "application/json");
        var response = await client.PostAsync(url, content);
        
        var responseString = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseString);
    }
}