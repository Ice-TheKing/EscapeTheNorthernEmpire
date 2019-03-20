using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    const float BASIC_SCALING_FACTOR = 10.0f;

    public float MoveSpeed = 1.0f;
    public float JumpSpeed = 10.0f;

    public float coolDown = 0.0f;

    public bool intelCollected = false;

    public Bullet ForceShot;

    Rigidbody2D rb;

    // animator
    Animator anim;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //check cool downs and decrease them as necessary
        if(coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left * BASIC_SCALING_FACTOR * MoveSpeed);
            transform.localScale = new Vector3(-1, 1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * BASIC_SCALING_FACTOR * MoveSpeed);
            transform.localScale = new Vector3(1, 1);
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && coolDown <= 0)
        {
            rb.AddForce(Vector2.up * BASIC_SCALING_FACTOR * JumpSpeed);
            coolDown = .5f;
        }

        //restart level cheat
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            var relativeTarget = target - transform.position;
            var angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;
            var direction = Quaternion.AngleAxis(angle, Vector3.forward);
            var bullet = Instantiate(ForceShot, transform.position, direction);
            Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        // check speed and feed the result into the animator so it can choose an animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}

