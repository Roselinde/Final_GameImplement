using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour {

    public GameObject Key1;
    public GameObject Key2;
    public GameObject Key3;
    public GameObject Key4;
    public GameObject Key5;
    public GameObject Key6;
    public GameObject Key7;
    public GameObject Key8;
    public GameObject Key9;
    public GameObject Key10;
    public GameObject Key11;
    public GameObject Key12;
    public GameObject Key13;

    // Use this for initialization
    void Start () {
        Key1.SetActive(false);
        Key2.SetActive(false);
        Key3.SetActive(false);
        Key4.SetActive(false);
        Key5.SetActive(false);
        Key6.SetActive(false);
        Key7.SetActive(false);
        Key8.SetActive(false);
        Key9.SetActive(false);
        Key10.SetActive(false);
        Key11.SetActive(false);
        Key12.SetActive(false);
        Key13.SetActive(false);

        var randomint = Random.Range(0, 12);

        int KeyEnable = randomint;

        switch (KeyEnable)
        {
            case 0:
                Key1.SetActive(true);
                break;
            case 1:
                Key2.SetActive(true);
                break;
            case 2:
                Key3.SetActive(true);
                break;
            case 3:
                Key4.SetActive(true);
                break;
            case 4:
                Key5.SetActive(true);
                break;
            case 5:
                Key6.SetActive(true);
                break;
            case 6:
                Key7.SetActive(true);
                break;
            case 7:
                Key8.SetActive(true);
                break;
            case 8:
                Key9.SetActive(true);
                break;
            case 9:
                Key10.SetActive(true);
                break;
            case 10:
                Key11.SetActive(true);
                break;
            case 11:
                Key12.SetActive(true);
                break;
            case 12:
                Key13.SetActive(true);
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
