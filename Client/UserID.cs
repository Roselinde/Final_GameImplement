using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Newtonsoft.Json;

public class UserID
{
    [JsonProperty("id")]
    public int id { get; set; }
    [JsonProperty("name")]
    public string name { get; set; }
    [JsonProperty("password")]
    public string password { get; set; }
    [JsonProperty("score")]
    public int score { get; set; }


}
