using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class Post
{
    public int userId { get; set; }
    public int id { get; set; }
    public string title { get; set; }
    public string body { get; set; }
}
namespace Assingment_HTTP
{
     class Program
    {
        static async Task Main(string[] args)
        {
            //GET
            Console.WriteLine("hello world");
            string url = "https://jsonplaceholder.typicode.com/posts";
            HttpClient client = new HttpClient();
            var httpresponse = await client.GetAsync(url);
            string jsonresponse = await httpresponse.Content.ReadAsStringAsync();
            Console.WriteLine(jsonresponse);

            var myposts = JsonConvert.DeserializeObject<List<Post>>(jsonresponse);

            //POST
            var newpost = new Post()
            {
                userId = 5,
                id = 6,
                title = "hi i created this object",
                body = "hi this is my body of the object"

            };
            var enpoint = new Uri("https://jsonplaceholder.typicode.com/posts");
            var json = JsonConvert.SerializeObject(newpost);
            var payload = new StringContent(json, Encoding.UTF8, "application/json");
            var result = client.PostAsync(enpoint, payload).Result.Content.ReadAsStringAsync().Result;
            if (result != null)
            {
                Console.WriteLine("succeeded");
                Console.WriteLine(result);
            }

            foreach (var post in myposts)
            {

                Console.WriteLine($"{post.userId} {post.id} {post.title} {post.body}");
            }


            //PUT data
            Console.WriteLine("putting the data in the placeholder");
            Console.WriteLine("putting the data in the placeholder");

            // Update the post with id 6
            var postToUpdate = new Post()
            {
                userId = 5,
                id = 6,
                title = "Updated Title",
                body = "Updated body content"
            };

            var putEndpoint = new Uri("https://jsonplaceholder.typicode.com/posts/6");
            var putJson = JsonConvert.SerializeObject(postToUpdate);
            var putPayload = new StringContent(putJson, Encoding.UTF8, "application/json");
            var putResponse = await client.PutAsync(putEndpoint, putPayload);
            var putResult = await putResponse.Content.ReadAsStringAsync();

            if (putResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("PUT request succeeded");
                Console.WriteLine(putResult);
            }
            else
            {
                Console.WriteLine($"PUT request failed with status code: {putResponse.StatusCode}");
            }
            Console.WriteLine("deleting the post with id 6");

            var deleteEndpoint = new Uri("https://jsonplaceholder.typicode.com/posts/6");
            var deleteResponse = await client.DeleteAsync(deleteEndpoint);

            if (deleteResponse.IsSuccessStatusCode)
            {
                Console.WriteLine("DELETE request succeeded");
            }
            else
            {
                Console.WriteLine($"DELETE request failed with status code: {deleteResponse.StatusCode}");
            }

            Console.ReadLine();

        }
    }
}
