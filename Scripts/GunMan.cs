using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GunMan : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bulletSpeed = 7;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shoot());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator shoot()
    {
        while (true)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position + Vector3.left * 0.6f + Vector3.up * 0.1f, transform.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.isKinematic = true;
            bulletRb.velocity = transform.rotation * Vector2.left * bulletSpeed;
            yield return new WaitForSeconds(2f);
        }
    }
}
