using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithRange : MonoBehaviour
{
    private Transform target;
    [SerializeField] private string tagName;

    private float range = 30f;
    public float turnSpeed = 10f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    private float fireRate = 2f;
    private float fireCountDown =  2f;
    private float bulletSpeed = 15f;
    //public Transform partToRotate;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
	{
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tagName);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

		foreach (GameObject enemy in enemies)
		{
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        if(target == null)
		{
            return;
		}

        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = fireRate;
        }

        fireCountDown -= Time.deltaTime;

        LockOnTarget();
    }

    void LockOnTarget()
	{
        Vector2 dir = new Vector2(target.position.x,target.position.y) - new Vector2(transform.position.x, transform.position.y);
        transform.right = dir;
    }

    void Shoot()
	{
        GameObject bulletPrefabGO = Instantiate(bulletPrefab, firePoint.position,Quaternion.identity);
        bulletPrefabGO.GetComponent<Rigidbody2D>().AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
        Destroy(bulletPrefabGO, 4f);
    }
}
