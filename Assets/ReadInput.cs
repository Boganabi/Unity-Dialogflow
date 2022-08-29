using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ReadInput : MonoBehaviour
{
    private string input;

    // Start is called before the first frame update
    void Start()
    {
        Task.Run(() => RunAsync());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s)
    {
        input = s;
        Debug.Log(input);
        
    }

    static async Task RunAsync()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://127.0.0.1:5000/");
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("input", "Hello")
            });
            var result = await client.PostAsync("", content);
            string resultContent = await result.Content.ReadAsStringAsync();
            Debug.Log(resultContent);
        }
    }
}

class Product
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string Category { get; set; }
}