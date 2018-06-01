using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class HSManager : MonoBehaviour {

    private static HSManager instance;

    public string updateScoreURL;
    public string loadScoreURL;

    [SerializeField]
    private int score;
    [SerializeField]
    private int highScore;


    public int HighScore
    {
        get
        {
            return highScore;
        }

        set
        {
            highScore = value;
        }
    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }
    public static HSManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HSManager>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    // Use this for initialization
    void Start () {
        highScore = PlayerPrefs.GetInt("Highscore");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateScore()
    {
        updateScoreURL = "http://ec2-13-229-197-129.ap-southeast-1.compute.amazonaws.com:8081/user/update?name="+LoginScripts.currentUser+"&score="+highScore;
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(updateScoreURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();


        }
        catch (WebException ex)
        {
            Debug.LogError(ex);
            SceneNavigation.Instance.NotificationText.text = "Error with connection to server";
        }
    }

    public void LoadScore()
    {
        loadScoreURL = "http://ec2-13-229-197-129.ap-southeast-1.compute.amazonaws.com:8081/user/loadscore/" + LoginScripts.currentUser;

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(loadScoreURL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();

            UserID[] userIDs = JsonConvert.DeserializeObject<UserID[]>(responseBody);
            print(userIDs[0].name);

            highScore = userIDs[0].score;

            PlayerPrefs.SetInt("Highscore", highScore);

        }
        catch (WebException ex)
        {
            Debug.LogError(ex);
            SceneNavigation.Instance.NotificationText.text = "Failed to load data from cloud save";
        }
        
    }
}
