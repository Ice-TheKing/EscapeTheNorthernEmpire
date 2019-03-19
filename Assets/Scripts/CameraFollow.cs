using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // what transform the camera should follow
    public Transform target;

    // where the camera sits offset from the target's position
    public Vector3 zoomOffset = new Vector3( 0, 2, -10 );

    // speed at which the camera should smoothly follow it's target
    public float smoothingSpeed = 0.25f;

    void FixedUpdate() {
        // where the camera wants to be
        Vector3 desiredPosition = target.position + zoomOffset;

        // where the camera will be due to smoothing (linear interpolation)
        Vector3 smoothedPosition = Vector3.Lerp( transform.position, desiredPosition, smoothingSpeed * Time.deltaTime );

        // set the position equal to the smoothed out position
        transform.position = smoothedPosition;
    }


}
