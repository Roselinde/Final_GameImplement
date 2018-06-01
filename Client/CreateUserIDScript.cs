using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class CreateUserIDScript : MonoBehaviour {

    private static CreateUserIDScript instance;

    public string URL_ADD;
    public InputField userIDEnter;
    public InputField passwordEnter;

    private string UserIDCreate;
    private string EnterUserID;
    private string EnterPassword;

    public static CreateUserIDScript Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<CreateUserIDScript>();
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

   
    void Update()
    {

    }

    public void AddUserID()
    {

        EnterUserID = userIDEnter.text;
        EnterPassword = passwordEnter.text;

        //int ranNumber = Random.Range(1, 999);

        int emptyScore = 0;

        UserIDCreate = "http://ec2-13-229-197-129.ap-southeast-1.compute.amazonaws.com:8081/user/add?name=" + EnterUserID + "&password=" + EnterPassword + "&score=" + emptyScore;
        URL_ADD = UserIDCreate;

        Debug.Log("" + UserIDCreate);

        Debug.Log(URL_ADD);
       try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL_ADD);
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
}
