using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour
{
    public float DragWhilePulling = 5;
    public float AngularDragWhilePulling = 5;
    public PlayerControls Player;

    private Rigidbody2D rb;

    private float PullForceMultiplier;
    private bool dragging = false;
    // you can click and drag from any point in an object, so we track where in the object
    // you clicked and drag from that point
    private Vector3 dragHandle;
    // we increase the drag when pulling so things don't bounce around crazily, so this
    // stores the old value
    private float oldDrag;
    // likewise for the angular drag
    private float oldAngularDrag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
            if (Player == null)
            {
                throw new NullReferenceException("Player was not specified and no object with Player tag found");
            }
        }
        PullForceMultiplier = Player.PullStrength;
    }

    // Update runs BEFORE OnMouse{Up,Down}, so we need to make sure to code for that
    // in this case, it means we 'react' a frame late to the click, and the release,
    // and frankly, I think that's fine for this interaction
    void Update()
    {
        if (dragging)
        {
            var clickPoint = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)
            );
            var clickForce = clickPoint - transform.TransformPoint(dragHandle);
            rb.AddForceAtPosition(clickForce * PullForceMultiplier, clickPoint);
        }
    }

    // specifically when THIS OBJECT is clicked
    void OnMouseDown()
    {
        dragging = true;
        var clickPoint = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z)
        );
        dragHandle = transform.InverseTransformPoint(clickPoint);
        oldDrag = rb.drag;
        rb.drag = DragWhilePulling;
        oldAngularDrag = rb.angularDrag;
        rb.angularDrag = AngularDragWhilePulling;
    }

    // when the mouseclick indicated in OnMouseDown ends -- not just when the mouse
    // goes up over this object.
    void OnMouseUp()
    {
        rb.drag = oldDrag;
        rb.angularDrag = oldAngularDrag;
        dragging = false;
    }
}
