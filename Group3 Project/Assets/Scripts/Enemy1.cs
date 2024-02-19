using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    Animator animator;
    public Rigidbody2D rb;
    public bool PlayerFound = false;

    public GameObject Bullet;
    public GameObject BulletSpawn;

    float speed;
    public int HP;
    Vector2 TargetDirection;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("StartPatrol");
    }

    private IEnumerator StartPatrol()
    {
        while (PlayerFound == false)
        {
            yield return new WaitForSeconds(1f);
            //will work on this later when i have time lol
            // enemy patrol in random direction
            rb.velocity = transform.up * speed;
            RandomDirectionChange();

        }

        while (PlayerFound == true)
        {
            Instantiate(Bullet, BulletSpawn.transform.position, Quaternion.identity);
            
            // roll in one of 4 directions (does not work)
            /*float rollDirection = Random.Range(1f, 5f);
            if (rollDirection == 1)
            {
                rb.AddForce(new Vector2(10, 0));
            }
            if (rollDirection == 2)
            {
                rb.AddForce(new Vector2(-10, 0));
            }
            if(rollDirection == 3)
            {
                rb.AddForce(new Vector2(0, 10));
            }
            if(rollDirection == 4)
            {
                rb.AddForce(new Vector2(0, -10));
            } */
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {// when player enters the trigger range and gets detected by enemy
        if (collision.gameObject.tag == "Player")
        {
            PlayerFound = true;
            
        }
        else
        {
            PlayerFound = false;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Quaternion rotation = Quaternion.LookRotation(collision.transform.position - transform.position);
            transform.rotation = rotation * Quaternion.Euler(0, 90, 0);
        }
    }

    /*public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerFound = false;
        }
    }*/

    private void RandomDirectionChange()
    {
        float ChangeDirectionCooldown = 0;
        ChangeDirectionCooldown -= Time.deltaTime;

        if(ChangeDirectionCooldown <= 0)
        {
            float AngleChange = Random.Range(-90f, 90f);
            Quaternion rotation = Quaternion.AngleAxis(AngleChange, transform.forward);
            TargetDirection = rotation * TargetDirection;

            ChangeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            hpUpdate();
            Destroy(collision.gameObject);
            if(HP<= 0)
            {
                animator.SetTrigger("isDead");
                
                Destroy(gameObject,0.6f);
            }
        }
    }

    public void hpUpdate()
    {
        HP--;
    }
}
