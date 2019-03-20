using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    const float BASIC_SCALING_FACTOR = 10.0f;

    public float MoveSpeed = 1.0f;
    public float JumpSpeed = 10.0f;

    public float JumpCoolDown = 0.5f;
    public float ShotCoolDown = 0.5f;

    private float jumpCoolDown = 0.0f;
    private float shotCoolDown = 0.0f;

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
        if(jumpCoolDown > 0)
        {
            jumpCoolDown -= Time.deltaTime;
        } else {
            // jump is over so stop the animation
            anim.SetBool("Jumping", false);
        }

        if (shotCoolDown > 0)
        {
            shotCoolDown -= Time.deltaTime;
        } else
        {
            if (Input.GetMouseButtonDown(1))
            {
                shotCoolDown = ShotCoolDown;
                Vector2 spawnPosition = transform.position;
                spawnPosition.y += .2f;

                var target = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                var relativeTarget = target - transform.position;
                var angle = Mathf.Atan2(relativeTarget.y, relativeTarget.x) * Mathf.Rad2Deg;
                var direction = Quaternion.AngleAxis(angle, Vector3.forward);
                var bullet = Instantiate(ForceShot, spawnPosition, direction);
                Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(), GetComponent<Collider2D>());

                // set animator to trigger the push animation
                anim.SetTrigger("Push");
            }
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && jumpCoolDown <= 0)
        {
            rb.AddForce(Vector2.up * BASIC_SCALING_FACTOR * JumpSpeed);
            jumpCoolDown = JumpCoolDown;

            // start jumping animation
            anim.SetBool("Jumping", true);
        }

        //restart level cheat
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        

        // check speed and feed the result into the animator so it can choose an animation
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
}

