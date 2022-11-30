using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;

    private float bulletSpeed = 20f;
    private float fireRate;
    private float startFireRate = 0.5f;
    void Start()
    {
        fireRate = startFireRate;
    }

    void Update()
    {
		if (Input.GetMouseButton(0) && fireRate <= 0)
		{
            fireRate = startFireRate;
            ShootBullet();
		}

        fireRate -= Time.deltaTime;
    }

    void ShootBullet()
	{
        FindObjectOfType<AudioManager>().Play("Shoot");
        GameObject bulletPrefabGO = Instantiate(bulletPrefab,firePoint.position,Quaternion.identity);
        bulletPrefabGO.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletSpeed,ForceMode2D.Impulse);
        Destroy(bulletPrefabGO,4f);
	}

}
