using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemies;

    private int maxX = 35;
    private int maxY = 35;
    [SerializeField] private float randomMaxRate;

    private string[] themes = {"Theme2", "Theme3" , "Theme4" };
    void Start()
    {
        FindObjectOfType<AudioManager>().Play(themes[Random.Range(0,themes.Length)]);

        if(enemies.Length <= 0)
		{
            return;
		}

        StartCoroutine(SpawnEnemy(3f));
    }

    public void StopSpawnEnemy()
	{
        EnemyHealth[] aliveEnemies = GameObject.FindObjectsOfType<EnemyHealth>();
		foreach (EnemyHealth enemy in aliveEnemies)
		{
            enemy.EnemyDead();
		}
        StopAllCoroutines();
	}

    IEnumerator SpawnEnemy(float time)
	{
        yield return new WaitForSeconds(time);
        float randomRate = Random.Range(0f,randomMaxRate);
        int randomIndex = Random.Range(0,enemies.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-maxX,maxX), Random.Range(-maxY,maxY),0f);
        Instantiate(enemies[randomIndex],spawnPosition,Quaternion.identity);
        yield return new WaitForSeconds(randomRate);
        StartCoroutine(SpawnEnemy(0f));
    }
}
