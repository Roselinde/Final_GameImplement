using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour {

    private static PlayerController instance;

    public GameObject player;
	public float speed;
	public Text keyText;
	public Text winText;
    public Text questText;
    public Text timerText;
    public Text warnText;
    public GameObject restart;
    public GameObject mainmenu;
    public GameObject highscore;
    public float timer = 0;
    public float maxspeed = 30f;
    public AudioSource Source;
    public AudioClip Horde;
    public AudioClip Die1;
    public AudioClip Die2;
    public AudioClip Die3;
    public AudioClip Die4;
    public AudioClip Die5;
    public AudioClip Die6;

    private bool hordeTrigger = false;


    private Rigidbody rb;
	private int KeyCount;
    private int deadint;

    public static PlayerController Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    void Awake()
    {
        StartCoroutine(QuestText());
    }

    void Start ()
	{
        player.SetActive(true);
        rb = GetComponent<Rigidbody>();
		KeyCount = 0;
		SetKeyText ();
		winText.text = "";
        warnText.text = "";
        restart.SetActive(false);
        mainmenu.SetActive(false);
        highscore.SetActive(false);
	}

    void Update()
    {
        timer += Time.deltaTime;
        timerText.text = "Time: " + Mathf.Floor(timer).ToString();
        if(hordeTrigger == false)
        {
            if (timer > 60)
            {
                StartCoroutine(HordeIncoming());
                hordeTrigger = true;
            }
        }


    }

    void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);

        if (rb.velocity.magnitude > maxspeed)
        {
            rb.velocity = rb.velocity.normalized * maxspeed;
        }
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			other.gameObject.SetActive (false);

			KeyCount = KeyCount + 1;

			SetKeyText ();
		}


    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Zombie")
        {
            warnText.text = "You died";
            restart.SetActive(true);
            mainmenu.SetActive(true);
            highscore.SetActive(true);
            questText.text = " ";

            HSManager.Instance.Score = 100 - (Mathf.FloorToInt(timer));
            if (HSManager.Instance.HighScore < HSManager.Instance.Score)
            {              
                HSManager.Instance.HighScore = HSManager.Instance.Score;
                PlayerPrefs.SetInt("Highscore", HSManager.Instance.HighScore);
                HSManager.Instance.UpdateScore();
            }
            

            if (HSManager.Instance.Score <= 0)
            {
                HSManager.Instance.Score = 0;
            }

            winText.text = "Score : " + HSManager.Instance.Score.ToString();

            player.SetActive(false);

            int randomdeadint = Random.Range(0, 5);
            deadint = randomdeadint;
            switch (deadint)
            {
                case 0:
                    Source.PlayOneShot(Die1);
                    break;
                case 1:
                    Source.PlayOneShot(Die2);
                    break;
                case 2:
                    Source.PlayOneShot(Die3);
                    break;
                case 3:
                    Source.PlayOneShot(Die4);
                    break;
                case 4:
                    Source.PlayOneShot(Die5);
                    break;
                case 5:
                    Source.PlayOneShot(Die6);
                    break;
            }
            
        }

        if (collision.gameObject.tag == "Exit")
            if (KeyCount >= 1)
            {

                warnText.text = "You survived";
                restart.SetActive(true);
                mainmenu.SetActive(true);
                highscore.SetActive(true);

                HSManager.Instance.Score = 200 - (Mathf.FloorToInt(timer));
                winText.text = "Score : " + HSManager.Instance.Score.ToString();

                
                if(HSManager.Instance.Score > HSManager.Instance.HighScore)
                {
                    HSManager.Instance.HighScore = HSManager.Instance.Score;
                    PlayerPrefs.SetInt("Highscore", HSManager.Instance.HighScore);
                    HSManager.Instance.UpdateScore();
                }
                player.SetActive(false);
            }
    }

    void SetKeyText()
	{
		keyText.text = "Key: Not Obtained ";

		if (KeyCount >= 1) 
		{

            StartCoroutine(GotKey());
		}
	}


    IEnumerator GotKey()
    {
        keyText.text = "Key: Obtained!";
        winText.text = "Get to the exit!";
        yield return new WaitForSeconds(5);
        winText.text = " ";
    }

    IEnumerator HordeIncoming()
    {
        warnText.text = "YOU WILL NOT SURVIVE";
        Source.PlayOneShot(Horde);
        yield return new WaitForSeconds(6);
        warnText.text = "";
    }

    IEnumerator QuestText()
    {
        questText.text = "Find the key to exit the city.";
        yield return new WaitForSeconds(5);
        questText.text = " ";
    }
}