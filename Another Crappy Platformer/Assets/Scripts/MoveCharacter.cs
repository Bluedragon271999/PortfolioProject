using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveCharacter : MonoBehaviour
{
    // Movement Properties
    public float moveSpeed = 15;
    [Range(0, 1)] public float sliding = 0.9f;
    public float jumpForce = 1200;
    int score;
    int health;
    public Text yourText;
    public Text healthText;
    void Start()
    {
        score = 0;
        health = 3;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gems"))
        {
            score += 100;
            yourText.text = "Score: " + score;
        }
        if (other.gameObject.CompareTag("bottom"))
        {
            health -= 1;
            if(health <= 0)
            {
                SceneManager.LoadScene("Lose");
            }
            healthText.text = "Lives: " + health;
        }
    }
    bool IsGrounded()
    {
        // noobtuts.com isGrounded function

        // Get Bounds and Cast Range (10% of height)
        Bounds bounds = GetComponent<Collider2D>().bounds;
        float range = bounds.size.y * 0.1f;

        // Calculate a position slightly below the collider
        Vector2 v = new Vector2(bounds.center.x,
                                bounds.min.y - range);

        // Linecast upwards
        RaycastHit2D hit = Physics2D.Linecast(v, bounds.center);

        // Was there something in-between, or did we hit ourself?
        return (hit.collider.gameObject != gameObject);
    }

    void FixedUpdate()
    {
        // Horizontal Movement
        float h = Input.GetAxis("Horizontal");
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        if (h != 0)
        {
            // Move Left/Right
            GetComponent<Rigidbody2D>().velocity = new Vector2(h * moveSpeed, v.y);
            transform.localScale = new Vector2(Mathf.Sign(h), transform.localScale.y);
        }
        else
        {
            // Get slower (Super Mario style sliding motion)
            GetComponent<Rigidbody2D>().velocity = new Vector2(v.x * sliding, v.y);
        }
        //Vertical Movement(Jumping)
        bool grounded = IsGrounded();
        if (Input.GetKey(KeyCode.UpArrow) && grounded)
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
    }
}
