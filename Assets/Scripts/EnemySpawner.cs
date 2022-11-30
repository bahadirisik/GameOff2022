using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyLittleOne;
	private float randomWaitTime = 2f;
	//private int randomEnemyCount = 1;

	private void Start()
	{
		randomWaitTime = 2f;
		StartCoroutine(SpawnEnemy());
	}

	IEnumerator SpawnEnemy()
	{
		randomWaitTime = Random.Range(5f,8f);
		Instantiate(enemyLittleOne, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(randomWaitTime);
		StartCoroutine(SpawnEnemy());
	}

}
