using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Respawn : MonoBehaviour {

    public Vector3 deathPos;
    //public Transform player;
    public Vector3 respawnPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(this.transform.position.y < deathPos.y)
        {
            //reset level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}
}
