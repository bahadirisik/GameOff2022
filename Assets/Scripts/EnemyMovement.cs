using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Vector2 dir;
    Vector2 wavePos;

    private float enemySpeed;
    [SerializeField] private float enemyStartSpeed = 3f;
    private Rigidbody2D enemyRB;

    [SerializeField] private bool waveMovement;
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();
        enemySpeed = enemyStartSpeed;
        if (FindObjectOfType<GameManager>().isPlayerDead)
            return;


        target = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
		{
            return;
		}

		if (waveMovement)
		{
            wavePos.y = transform.position.y + Mathf.Sin(Time.time * 5f) * 0.05f;
            transform.position = new Vector3(transform.position.x, wavePos.y, transform.position.z);
		}

        dir = target.position - transform.position;
    }

	private void FixedUpdate()
	{
        enemyRB.velocity = dir.normalized * enemySpeed;
	}

}
