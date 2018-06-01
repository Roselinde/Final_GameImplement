using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFollow : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject GetPlayer;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(GetPlayer.transform.position);
    }

    void OnCollisionEnter(Collider other)
    {
        if(other.tag == "DestroyZombie")
        {
            Destroy(gameObject);
        }
    }

}
