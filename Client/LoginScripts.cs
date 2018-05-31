using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class LoginScripts : MonoBehaviour {

    private static LoginScripts instance;

    public string URL;
    public InputField UserIDEnter;
    public InputField passwordEnter;

	private string loginUser;
    public string UserIDkey { get; set; }
    private string passwordkey;

    public LeaderBoardScript leaderboard;

    public static LoginScripts Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<LoginScripts>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }



    void Start()
    {

    }

    // Update is called once per frame
    public void LoginToBorad()
    {

        UserIDkey = UserIDEnter.text;
        passwordkey = passwordEnter.text;
        
		loginUser = "http://localhost:8081/user/login/" + UserIDkey;
		URL = loginUser;


        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            string responseBody = new StreamReader(stream).ReadToEnd();

            print(UserIDkey);

            UserID[] userIDs = JsonConvert.DeserializeObject<UserID[]>(responseBody);
            print(userIDs[0].name);


			if(UserIDkey == userIDs[0].name)
			{
				if(passwordkey == userIDs[0].password)
				{
					
					print("True");
                    SceneNavigation.Instance.StartCoroutine(SceneNavigation.Instance.LoginSuccess());

                    if (PlayerPrefs.HasKey("Highscore"))
                    {
                        PlayerPrefs.GetInt("Highscore", PlayerController.Instance.HighScore);
                    }
                    else
                    {
                        PlayerController.Instance.HighScore = 0;
                    }

                    SceneNavigation.Instance.HighScoreText.text = "Highscore : " + PlayerController.Instance.HighScore.ToString();
                }
				else
				{
                    SceneNavigation.Instance.StartCoroutine(SceneNavigation.Instance.LoginFail());

                    print("passF");
				}

			}
			else
			{
                SceneNavigation.Instance.StartCoroutine(SceneNavigation.Instance.LoginFail());
                print("userF");

			}
        }
        catch (WebException ex)
        {

        }
       
    }
}
