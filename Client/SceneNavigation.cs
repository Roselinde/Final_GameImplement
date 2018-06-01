using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigation : MonoBehaviour
{

    private static SceneNavigation instance;

    public GameObject MainMenu;
    public GameObject login;
    public GameObject register;
    public GameObject loadSave;
    public GameObject CreditPopUp;
    public GameObject highScorePopUp;
    public GameObject newGameWarnPopUp;
    public GameObject loadGamePopUp;

    [SerializeField]
    private InputField usernameLogin;
    [SerializeField]
    private InputField passwordLogin;
    [SerializeField]
    private InputField usernameRegister;
    [SerializeField]
    private InputField passwordRegister;
    [SerializeField]
    private Text usernameRegisterText;
    [SerializeField]
    private Text passwordRegisterText;
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Text notificationText;

    public Button loadButton;

    private static bool inLoginScreen = false;
    private static bool inRegisterScreen = false;
    private Text _highScoreText;

    public bool PlayOffline { get; set; }

    public static SceneNavigation Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SceneNavigation>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public Text HighScoreText
    {
        get
        {
            return highScoreText;
        }

        set
        {
            highScoreText = value;
        }
    }

    public Text NotificationText
    {
        get
        {
            return notificationText;
        }

        set
        {
            notificationText = value;
        }
    }

    void Start()
    {
        CreditPopUp.SetActive(false);
        HighScoreText.text = "";
    }
    // Use this for initialization

    public void OnStartButtonClicked()
    {
        MainMenu.SetActive(false);
        login.SetActive(true);
        loadSave.SetActive(false);
        NotificationText.text = "";
        inLoginScreen = true;
    }

    public void OnLoginButtonClicked()
    {
        NotificationText.text = "";
        if (usernameLogin.text == "" || passwordLogin.text == "")
        {
            StartCoroutine(PleaseInputSomethingLogin());
        }
        else
        {
            loadButton.interactable = true;
            LoginScripts.Instance.LoginToBorad();
        }

    }

    public void OnRegisterButtonClicked()
    {
        login.SetActive(false);
        register.SetActive(true);

        inLoginScreen = false;
        inRegisterScreen = true;
    }

    public void OnRegisterConfirmButtonClicked()
    {
        if (usernameRegisterText.text != "" && passwordRegisterText.text != "")
        {
            CreateUserIDScript.Instance.AddUserID();

            NotificationText.color = new Color(0, 255, 0);
            NotificationText.text = "Register Successfully as : " + usernameRegisterText.text;

            login.SetActive(true);
            register.SetActive(false);

            inRegisterScreen = false;
            inLoginScreen = true;
        }
        else
        {
            StartCoroutine(PleaseInputSomethingRegister());
        }
    }

    public void OnRegisterCancelButtonClicked()
    {
        login.SetActive(true);
        register.SetActive(false);

        NotificationText.text = "";

        inRegisterScreen = false;
        inLoginScreen = true;

    }

    public void OnPlayOfflineButtonClicked()
    {
        if (PlayerPrefs.HasKey("Highscore"))
        {
            HSManager.Instance.HighScore = PlayerPrefs.GetInt("Highscore");
        }
        else
        {
            HSManager.Instance.HighScore = 0;
        }

        loadButton.interactable = false;

        HighScoreText.text = "Highscore : " + HSManager.Instance.HighScore.ToString();

        loadSave.SetActive(true);
        MainMenu.SetActive(false);
        login.SetActive(false);

        PlayOffline = true;

        inLoginScreen = false;

        NotificationText.color = new Color(0, 255, 0);
        NotificationText.text = "Play offline";
    }

    public void OnNewGameButtonClicked()
    {
        newGameWarnPopUp.SetActive(true);
    }

    public void OnNewGameConfirmButtonClicked()
    {
        SceneManager.LoadScene("Zombie Survival");
    }

    public void OnNewGameCancelButtonClicked()
    {
        newGameWarnPopUp.SetActive(false);
    }

    public void OnLoadButtonClicked()
    {
        loadGamePopUp.SetActive(true);
    }

    public void OnLoadGameConfirmButtonClicked()
    {
        HSManager.Instance.LoadScore();
        HighScoreText.text = "Highscore : " + HSManager.Instance.HighScore.ToString();
        loadGamePopUp.SetActive(false);
    }

    public void OnLoadGameCancelButtonClicked()
    {
        loadGamePopUp.SetActive(false);
    }

    public void OnReturnButtonClicked()
    {
        login.SetActive(false);
        loadSave.SetActive(false);
        MainMenu.SetActive(true);

        inLoginScreen = false;

        NotificationText.text = " ";
        HighScoreText.text = "";
    }

    public void OnRestartButtonClick()
    {

        SceneManager.LoadScene("Zombie Survival");
    }

    public void OnMainMenuButtonClick()
    {
        PlayerPrefs.DeleteKey("CurrentUser");
        SceneManager.LoadScene("Main Menu");
    }

    public void OnCreditButtonClick()
    {
        CreditPopUp.SetActive(true);
    }

    public void OnCloseButonClick()
    {
        //LeaderBoardScript.Instance.DelList();
        CreditPopUp.SetActive(false);
        highScorePopUp.SetActive(false);
        
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void OnHighscoreButtonClicked()
    {
        highScorePopUp.SetActive(true);
        LeaderBoardScript.Instance.ShowList();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void LoginSuccess()
    {
        NotificationText.color = new Color(0, 255, 0);
        NotificationText.text = "Login as : " + LoginScripts.Instance.UserIDkey;

        loadSave.SetActive(true);
        MainMenu.SetActive(false);
        login.SetActive(false);

    }

    public IEnumerator LoginFailUser()
    {
        NotificationText.color = new Color(255, 0, 0);
        NotificationText.text = "Username is incorrect.";
        yield return new WaitForSeconds(5);
        if (inLoginScreen)
        {
            NotificationText.text = " ";
            NotificationText.color = new Color(255, 255, 255);
        }
    }

    public IEnumerator LoginFailPassword()
    {
        NotificationText.color = new Color(255, 0, 0);
        NotificationText.text = "Password is incorrect.";
        yield return new WaitForSeconds(5);
        if (inLoginScreen)
        {
            NotificationText.text = " ";
            NotificationText.color = new Color(255, 255, 255);
        }
    }

    public IEnumerator PleaseInputSomethingRegister()
    {
        NotificationText.color = new Color(255, 0, 0);
        NotificationText.text = "Please enter Username/Password to register";
        yield return new WaitForSeconds(5);
        if (inRegisterScreen)
        {
            NotificationText.text = " ";
            NotificationText.color = new Color(255, 255, 255);
        }

    }

    public IEnumerator PleaseInputSomethingLogin()
    {
        NotificationText.color = new Color(255, 0, 0);
        NotificationText.text = "Please enter Username/Password to login";
        yield return new WaitForSeconds(5);
        if (inRegisterScreen)
        {
            NotificationText.text = " ";
            NotificationText.color = new Color(255, 255, 255);
        }

    }

    //For debugging

    public void OnDebugButtonClicked()
    {
        LeaderBoardScript.Instance.CheckDatabase();
    }
}
