using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int enemyStartHealth = 40;
    private int enemyHealth;
    [SerializeField] private GameObject friendlyRanged;
    [SerializeField] private GameObject enemyDeathEffect;
    [SerializeField] private Vector3 offset;

    public bool bossShield;
    [SerializeField] private float cameraShakeIntensity;
    [SerializeField] private float cameraShakeTime;

    [SerializeField] private string[] deathSounds;
    void Start()
    {
        bossShield = false;
        enemyHealth = enemyStartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth <= 0)
		{
            EnemyDead();
		}
    }

    public void EnemyGetsDamage(int damage)
	{
        if (bossShield)
            return;
        enemyHealth -= damage;
	}

    public void IncreaseEnemyHealth(int amount)
	{
        if (enemyHealth >= enemyStartHealth)
            return;
        enemyHealth += amount;
	}

    public void EnemyDead()
	{
        if(gameObject.tag == "RangedEnemy")
		{
            Instantiate(friendlyRanged,transform.position,Quaternion.identity);
		}

        GameObject enemyDeathEffectGO = Instantiate(enemyDeathEffect,transform.position + offset,Quaternion.identity);
        FindObjectOfType<GameManager>().UpdateEnemyKilledCount();
        FindObjectOfType<AudioManager>().Play(deathSounds[Random.Range(0,deathSounds.Length)]);
        CameraShake.instance.ShakeCamera(cameraShakeIntensity,cameraShakeTime);
        Destroy(enemyDeathEffectGO,3f);
        Destroy(gameObject);
	}

    public int GetEnemyHealth()
	{
        return enemyHealth;
	}
}
