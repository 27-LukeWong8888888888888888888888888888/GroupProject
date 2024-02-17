using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerProjectile : MonoBehaviour
{
    public Transform LazerSpawnPoint;
    void Start()
    {
        StartCoroutine(LazerDelay());
    }
    IEnumerator LazerDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
