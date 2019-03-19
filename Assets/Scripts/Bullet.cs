using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float Speed;
    public float Lifetime;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + transform.right);
        Lifetime -= Time.deltaTime;
        if (Lifetime < 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destructible") {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
