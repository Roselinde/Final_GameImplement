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

    [SerializeField]
	private string loginUser;
    public static string currentUser;
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
        loginUser = PlayerPrefs.GetString("CurrentUser");
        currentUser = loginUser;
    }

    // Update is called once per frame
    public void LoginToBorad()
    {

        UserIDkey = UserIDEnter.text;
        passwordkey = passwordEnter.text;
        
		URL = "http://ec2-13-229-197-129.ap-southeast-1.compute.amazonaws.com:8081/user/login/" + UserIDkey;
		loginUser = UserIDkey;
        currentUser = loginUser;

        PlayerPrefs.SetString("CurrentUser", currentUser);

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
                    SceneNavigation.Instance.LoginSuccess();

                    if (PlayerPrefs.HasKey("Highscore"))
                    {
                        HSManager.Instance.HighScore = PlayerPrefs.GetInt("Highscore");
                    }
                    else
                    {
                        HSManager.Instance.HighScore = 0;
                    }

                    SceneNavigation.Instance.HighScoreText.text = "Highscore : " + HSManager.Instance.HighScore.ToString();
                }
				else
				{
                    SceneNavigation.Instance.StartCoroutine(SceneNavigation.Instance.LoginFailPassword());

                    print("passF");
				}

			}
			else
			{
                SceneNavigation.Instance.StartCoroutine(SceneNavigation.Instance.LoginFailUser());
                print("userF");

			}
        }
        catch (WebException ex)
        {
            Debug.LogError(ex);

        }
       
    }
}
