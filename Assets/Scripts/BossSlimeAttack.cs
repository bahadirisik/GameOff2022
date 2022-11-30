using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlimeAttack : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] private Transform moveDir;
    [SerializeField] private GameObject slimeDeathEffect;
    private float slimeSpeed = 20f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(moveDir.right * slimeSpeed,ForceMode2D.Impulse);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
            collision.gameObject.GetComponent<PlayerHealth>().PlayerGetsDamage(40);
            FindObjectOfType<GameManager>().UptadeHealth();
            GameObject slimeDeathEffectGO = Instantiate(slimeDeathEffect,transform.position,Quaternion.identity);
            Destroy(slimeDeathEffectGO, 2f);
            Destroy(gameObject);
		}
	}

}
