using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ReadInput : MonoBehaviour
{
    public string replyJSON;
    public string reply;
    public string intent;
    private string s_id;

    // Start is called before the first frame update
    void Start()
    {
        // generate session id here
        Guid myuuid = Guid.NewGuid();
        s_id = myuuid.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadStringInput(string s) // called when the text box recives the enter key
    {
        replyJSON = RunAsync(s, s_id);
        reply = parseReply(replyJSON);
        intent = parseIntent(replyJSON);
        Debug.Log("reply: " + reply);
        Debug.Log("intent: " + intent);
    }

    private static string RunAsync(string inp, string uuid)
    {
        var para = new Dictionary<string, string>();

        /*
         * if the script is running on a server, put the address here. 
         * otherwise, the script is running on localhost and uses the address i put here
         */
        var url = "http://127.0.0.1:5000/";
        var result = "There was an error.";
        para.Add("input", inp);
        para.Add("session_id", uuid);
        para.Add("lang", "en-US"); // you can use this to change the language later

        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsync(url, new FormUrlEncodedContent(para)).Result;
            var token = response.Content.ReadAsStringAsync().Result;
            result = token;
            Debug.Log("ssid: " + uuid);
        }

        return result;
    }

    private static string parseReply(string json)
    {
        // Debug.Log(json);
        string[] jsonArr = json.Split('\n'); // index 1 is intent, 2 is reply
        // now our response looks like this: "response": "Welcome to my agent!"
        string[] res = jsonArr[2].Split(':');
        string finalString = res[1].Replace("\"", "");
        return finalString;
    }

    private static string parseIntent(string json)
    {
        string[] jsonArr = json.Split('\n'); // index 1 is intent, 2 is reply
        // now our intent looks like this: "intent": "Default Welcome Intent",
        string[] res = jsonArr[1].Split(':');
        string finalString = res[1].Replace("\"", "");
        string finalString2 = finalString.Replace(",", ""); // remove trailing comma
        return finalString2;
    }
    /* response payload looks like this: 
     * token: {
        "intent": "Default Welcome Intent", 
        "response": "Welcome to my agent!"
        }
     */
}
