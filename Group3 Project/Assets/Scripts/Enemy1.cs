using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool PlayerFound = false;

    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartPatrol");
    }

    private IEnumerator StartPatrol()
    {
        while (PlayerFound == false)
        {
            yield return new WaitForSeconds(2f);
            //will work on this later when i have time lol
            // enemy patrol in random direction
        }

        while (PlayerFound == true)
        {
            Instantiate(Bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Instantiate(Bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            Instantiate(Bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
            // roll or enemy moves to left or right
            
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {// when player enters the trigger range and gets detected by enemy
        if (collision.gameObject.tag == "Player")
        {
            PlayerFound = true;
            transform.LookAt(collision.transform);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            transform.LookAt(collision.transform);
        }
    }
}
