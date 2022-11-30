using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealer : MonoBehaviour
{
    private GameObject boss;
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss == null)
            return;

        StartCoroutine(HealEnemy());
    }

    IEnumerator HealEnemy()
	{
        boss.GetComponent<EnemyHealth>().IncreaseEnemyHealth(3);
        yield return new WaitForSeconds(2f);
        StartCoroutine(HealEnemy());
	}
}
