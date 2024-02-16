using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody2D rb;
    public Player PlayerScipt;

    private void Start()
    {
        PlayerScipt = GameObject.Find("Player").GetComponent<Player>();

        if (PlayerScipt.isFacingRight)
        {
            rb.velocity = transform.right * speed;
        }
        else if (!PlayerScipt.isFacingRight)
        {
            rb.velocity = -transform.right * speed;
            gameObject.transform.Rotate(0, 180, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            
            rb.velocity = Vector2.zero;
        }
    }
}
