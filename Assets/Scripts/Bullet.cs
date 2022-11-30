using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 10;
	[SerializeField] private GameObject playerBulletDeathEffect;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Enemy" || collision.tag == "RangedEnemy" || collision.tag == "Boss")
		{
			collision.gameObject.GetComponent<EnemyHealth>().EnemyGetsDamage(bulletDamage + PlayerStats.playerStrength);
		}

		GameObject playerBulletDeathEffectGO = Instantiate(playerBulletDeathEffect,transform.position,Quaternion.identity);
		Destroy(playerBulletDeathEffectGO,1f);
		Destroy(gameObject);
	}
}
