using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float ScrollScale;
    public Camera tracking;

    private float lastX;

    public float diff;
    
    void Start()
    {
        if (tracking == null) tracking = Camera.main;
        lastX = tracking.transform.position.x;
    }

    void Update()
    {
        diff = tracking.transform.position.x - lastX;
        transform.position += new Vector3(diff * ScrollScale, 0);
        lastX = tracking.transform.position.x;
    }
}
