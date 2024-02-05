using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform BulletSpawnpoint;
    private Animator Anim;

    [Header("Movement Variables")]
    private float horizontal;
    private float vertical;
    private float speed = 8f;
    public bool isFacingRight = true;

    [Header("Gun Variables")]
    public GameObject Bullet;
    public int MaxBullets = 25;
    public float reloadtime = 5.0f;
    public Slider ReloadBar;
    private bool CanShoot=true;
    private bool Isreloading = false;

   [Header("Shadow Variables")]
    public GameObject Shadow;
    public GameObject Temp;
    public bool CanTeleport = true;
    public bool ShadowIsCooldown = false;
    public Image ShadowCDImg;
    public Text ShadowCDTxt;//For the CD Txt UI
    public float ShadowCDTimer = 5.0f;

    [Header("Invis Variables")]
    public SpriteRenderer PlayerSprite;
    public bool CanInvis = true;
    public bool InvisisOnCD = false;
    public Image InvisCDImg;
    public Text InvisCDText;
    public float InvisCDTimer = 5.0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();

        ShadowCDTxt.gameObject.SetActive(false);
        ShadowCDImg.fillAmount = 0.0f;
        InvisCDText.gameObject.SetActive(false);
        InvisCDImg.fillAmount = 0.0f;

        ReloadBar.gameObject.SetActive(false);

    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Shoot();
        Teleport();
        Invis();

        if (InvisisOnCD == true)
        {
            InvisCoolDown();
        }

        if (ShadowIsCooldown == true)
        {
            ShadowTeleCD();
        }
        if (Isreloading == true)
        {
            Reloading();
        }
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
            Isreloading = true;
        }
        else
        {
            Anim.SetBool("IsReloading", false);
        }
    }
    private void Reloading()
    {
        reloadtime -= Time.deltaTime;
        if (reloadtime < 0.0f)
        {
            MaxBullets = 25;
            CanShoot = true;
            ReloadBar.gameObject.SetActive(false);
            reloadtime = 5.0f;
            Isreloading = false;
        }
        else
        {
            ReloadBar.gameObject.SetActive(true);
            ReloadBar.value = reloadtime;
        }
        
    }
    IEnumerator ShadowTele()//Function of the Teleportation
    {
        yield return new WaitForSeconds(2);
        gameObject.transform.position = Temp.transform.position;
        Destroy(Temp.gameObject);
        ShadowIsCooldown = true;
    }
    private void ShadowTeleCD()//Cooldown for using the Teleport Ability
    {
        ShadowCDTimer -=Time.deltaTime;
        if (ShadowCDTimer < 0.0f)
        {
            CanTeleport = true;
            ShadowIsCooldown = false;
            ShadowCDTxt.gameObject.SetActive(false);
            ShadowCDImg.fillAmount = 0.0f;
            ShadowCDTimer = 5.0f;
        }
        else
        {
            ShadowCDTxt.gameObject.SetActive(true);
            ShadowCDTxt.text = Mathf.RoundToInt(ShadowCDTimer).ToString();
            ShadowCDImg.fillAmount = ShadowCDTimer / ShadowCDTimer;
        }

    }
    IEnumerator InvisDuration()//Duration of the Invis Ability
    {
        yield return new WaitForSeconds(2);
        PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
        int layerdef = LayerMask.NameToLayer("Default");
        gameObject.layer = layerdef;
        CanInvis = false;
        InvisisOnCD = true;
    }
    private void InvisCoolDown()//Cooldown timer for the Invis Ability
    {
        InvisCDTimer -= Time.deltaTime;
        if (InvisCDTimer < 0.0f)
        {
            CanInvis = true;
            InvisisOnCD = false;
            InvisCDText.gameObject.SetActive(false);
            InvisCDImg.fillAmount = 0.0f;
            InvisCDTimer = 5.0f;
        }
        else
        {
            InvisCDText.gameObject.SetActive(true);
            InvisCDText.text = Mathf.RoundToInt(InvisCDTimer).ToString();
            InvisCDImg.fillAmount = InvisCDTimer / InvisCDTimer;
        }
    }
    
    private void Teleport()//For Player to Teleport
    {
        if (CanTeleport)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Temp = Instantiate(Shadow, BulletSpawnpoint.transform.position, Quaternion.identity);
                StartCoroutine(ShadowTele());
                CanTeleport = false;
            }
        }
    }
    private void Invis()//For the Player to go Invis
    {
        if (Input.GetKeyDown(KeyCode.E)&&CanInvis==true)
        {
            PlayerSprite.GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
            int LayerInvis = LayerMask.NameToLayer("Invis Layer");
            gameObject.layer = LayerInvis;
            StartCoroutine(InvisDuration());

        }
    }
}
