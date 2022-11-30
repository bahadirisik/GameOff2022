using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollideWithPlayer : MonoBehaviour
{
	[SerializeField] private int enemyDamage = 10;
	private void OnCollisionStay2D(Collision2D collision)
	{
		if(collision.collider.tag == "Player")
		{
			collision.collider.gameObject.GetComponent<PlayerHealth>().PlayerGetsDamage(enemyDamage - PlayerStats.playerShield);
			FindObjectOfType<GameManager>().UptadeHealth();
		}
	}
}
