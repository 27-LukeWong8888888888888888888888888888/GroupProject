using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    [Header("BossHP")]
    [SerializeField]
    public int MaxHP;
    public static int CurrHP = 100;
    public Slider BossHPBar;
    public Sprite State1, State2, State3, State4, State5;

    public Transform Target; // Look at target

    [Header("BossShooting")]
    public GameObject Bullet;
    public GameObject Lazer;
    public Transform BulletPos1;
    public Transform BulletPos2;

    public GameObject Lazzer;

    public float Bullettimer1;
    public float Bullettimer2;
    public float lazerTimer;

    [SerializeField]
    private bool isAttacking = false;

    void Start()
    {
        CurrHP = 100;
        isAttacking = false;
        Lazzer.SetActive(false);
    }
    void Update()
    {
        BossStateChecker();
        if (isAttacking==false)
        {
            Vector3 Look = transform.InverseTransformPoint(Target.transform.position);
            float angle = Mathf.Atan2(Look.y, Look.x) * Mathf.Rad2Deg + 90;
            transform.Rotate(0, 0, angle);
        }
        BossHPBar.value = CurrHP;

        Bullettimer1 += Time.deltaTime;
        if (Bullettimer1 >= 2)
        {
            Bullettimer1 = 0;
            shootBullet1();
        }
        Bullettimer2 += Time.deltaTime;
        if (Bullettimer2 >= 2)
        {
            Bullettimer2 = 0;
            shootbullet2();
        }
        lazerTimer += Time.deltaTime;
        if (lazerTimer >= 10)
        {
            lazerTimer = 0;
            StartCoroutine(shootingLazer());
        }


    }
    void shootBullet1()
    {
        Instantiate(Bullet, BulletPos1.transform.position, Quaternion.identity);
    }
    void shootbullet2()
    {
        Instantiate(Bullet, BulletPos2.transform.position, Quaternion.identity);

    }
    IEnumerator shootingLazer()
    {
        Lazzer.SetActive(true);
        yield return new WaitForSeconds(0.65f);
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        Lazzer.SetActive(false);
        isAttacking = false;
    }
    void BossStateChecker()
    {
        if ((CurrHP >= 80) && (CurrHP <= 100))
        {
            GetComponent<SpriteRenderer>().sprite = State1;
        }
        else if ((CurrHP >= 60) && (CurrHP <= 79))
        {
            GetComponent<SpriteRenderer>().sprite = State2;

        }
        else if ((CurrHP >= 40) && (CurrHP <= 59))
        {
            GetComponent<SpriteRenderer>().sprite = State3;

        }
        else if ((CurrHP >= 20) && (CurrHP <= 39))
        {
            GetComponent<SpriteRenderer>().sprite = State4;

        }
        else if ((CurrHP == 0))
        {
            GetComponent<SpriteRenderer>().sprite = State5;

        }
    }
}
