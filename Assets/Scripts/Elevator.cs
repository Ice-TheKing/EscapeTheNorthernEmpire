using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour {

    float xOffset = .1f;
    Vector3 tempPos;
    public GameObject platform;
    Vector3 endPos;
    bool active = false;
    public float speed = 1;

    // Use this for initialization
    void Start () {

        tempPos = this.transform.position;
        endPos = platform.transform.position;
        endPos.y += 4;
    }
	
	// Update is called once per frame
	void Update () {
		if(active && platform.transform.position.y <= endPos.y)
        {
            Vector3 temp = platform.transform.position;
            temp.y += speed * Time.deltaTime;
            platform.transform.position = temp;
        }
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            tempPos.x += xOffset;
            this.transform.position = tempPos;
            active = true;
        }
    }
}
