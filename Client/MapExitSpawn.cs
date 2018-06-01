using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapExitSpawn : MonoBehaviour
{

    public GameObject SouthGate;
    public GameObject NorthGate;
    public GameObject EastGate;
    public GameObject WestGate;

    // Use this for initialization
    void Start()
    {
        SouthGate.SetActive(false);
        NorthGate.SetActive(false);
        EastGate.SetActive(false);
        WestGate.SetActive(false);

        var randomint = Random.Range(0, 3);

        int GateEnable = randomint;

        switch (GateEnable)
        {
            case 0:
                WestGate.SetActive(true);
                break;
            case 1:
                SouthGate.SetActive(true);
                break;
            case 2:
                EastGate.SetActive(true);
                break;
            case 3:
                NorthGate.SetActive(true);
                break;
        }
    }
}

