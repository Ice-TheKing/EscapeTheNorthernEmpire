using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intel : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player)
        {
            player.GetComponent<PlayerControls>().intelCollected = true;
            Destroy(gameObject);

        }
       
    }
}
