using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
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
        }
        GameObject.Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Boss")
        {
            BossScript.CurrHP -= 2;
            Destroy(gameObject);
        }
    }
}
