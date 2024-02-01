using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform BulletSpawnpoint;

    //Movement Stuff
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    public bool isFacingRight = true;

    public GameObject Bullet;
    public int MaxBullets = 25;
    public int reloadtime = 10;
    private bool CanShoot=true;

    public GameObject Shadow;
    public GameObject Temp;
    public bool CanTeleport = true;

    public SpriteRenderer PlayerSprite;
    public bool CanInvis = true;

    private Animator Anim;

    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }
    void Update()
    { 
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Shoot();
        Teleport();
        Invis();
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        if (horizontal > 0 && !isFacingRight)
        {
            Anim.SetBool("IsRunning", true);
            Flip();
        }
        else if (horizontal < 0 && isFacingRight)
        {
            Anim.SetBool("IsRunning", true);
            Flip();
        }
        else if (vertical > 0.1)
        {
            Anim.SetBool("IsRunning", true);
        }
        else if (vertical < 0)
        {
            Anim.SetBool("IsRunning", true);
        }
        else if (horizontal == 0)
        {
            Anim.SetBool("IsRunning", false);
        }
    }
    private void Flip() //For the sprite to flip and face right side
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0);
    }

    private void Shoot()
    {
        if (MaxBullets > 0)
        {
            CanShoot = true;
        }
        else
        {
            CanShoot = false;
        }
        if (CanShoot==true) 
        {
            Anim.SetBool("IsShooting", true);
            if (Input.GetMouseButtonDown(0))
            {
                MaxBullets--;
                Instantiate(Bullet, BulletSpawnpoint.position, Quaternion.identity);
                Anim.SetBool("IsShooting", true);
            }
            else
            {
                Anim.SetBool("IsShooting", false);
            }
        }
        else
        {
            CanShoot = false;
            Anim.SetBool("IsShooting", false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Anim.SetBool("IsReloading", true);
            StartCoroutine("Reload");
        }
        else
        {
            Anim.SetBool("IsReloading", false);
        }
    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(5);
        MaxBullets = 25;
        CanShoot = true;
    }
    IEnumerator ShadowTele()
    {
        yield return new WaitForSeconds(2.5f);
        gameObject.transform.position = Temp.transform.position;
        Destroy(Temp.gameObject);
    }
    IEnumerator InvisCD()
    {
        
        yield return new WaitForSeconds(5);
        CanInvis = true;
    }
    
    private void Teleport()//For Player to Teleport
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
             Temp = Instantiate(Shadow, BulletSpawnpoint.transform.position, Quaternion.identity);
            StartCoroutine(ShadowTele());
        }
    }
    private void Invis()//For the Player to go Invis
    {
        if (Input.GetKeyDown(KeyCode.E)&&CanInvis==true)
        {
            PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
            CanInvis = false;
            StartCoroutine(InvisCD());
        }
        
    }


}
