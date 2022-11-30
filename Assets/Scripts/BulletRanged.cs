using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRanged : MonoBehaviour
{
	[SerializeField] private int bulletDamage = 20;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			collision.gameObject.GetComponent<PlayerHealth>().PlayerGetsDamage(bulletDamage - PlayerStats.playerShield);
			FindObjectOfType<GameManager>().UptadeHealth();
		}
		Destroy(gameObject);
	}
}
