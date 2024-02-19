using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBulletMovement : MonoBehaviour
{
    Rigidbody2D rb;

    public float speed;
    Transform Player;
    Vector2 direction;

    public Enemy1 enemy1;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        direction = (Player.transform.position - this.transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * speed;
        GameObject.Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);
        }
    }
}
