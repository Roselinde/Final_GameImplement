using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawn : MonoBehaviour {

    public GameObject Zombie;
    private Vector3 position;
    private int berserk = 1;

    // Use this for initialization
    void Awake()
    {
        StartCoroutine(ZombiePreset());
        StartCoroutine(ZombieCreate());
        StartCoroutine(ZombieSurprise());
    }

    void Start () {
        Zombie.GetComponent<GameObject>();
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
	}

    IEnumerator ZombieCreate()
    {
        if (berserk <= 120)
        {
            yield return new WaitForSeconds(1);
            position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
            Instantiate(Zombie, position, Quaternion.identity);
            berserk++;
            StartCoroutine(ZombieCreate());
        }

        else
        {
            yield return new WaitForSeconds(0.5f);
            position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
            Instantiate(Zombie, position, Quaternion.identity);
            StartCoroutine(ZombieCreate());
        }

    }

    IEnumerator ZombiePreset()
    {
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(20);
    }

    IEnumerator ZombieSurprise()
    {

        yield return new WaitForSeconds(12);
        position = new Vector3(Random.Range(-80f, 80f), 5, Random.Range(-80f, 80f));
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        Instantiate(Zombie, position, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(ZombieSurprise());

    }

    // Update is called once per frame
}

