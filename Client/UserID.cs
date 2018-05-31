using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserID
{
    private static UserID instance;

    public static UserID Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public string name { get; set; }
    public string password { get; set; }
    public int score { get; set; }
    public int id { get; set; }


}
