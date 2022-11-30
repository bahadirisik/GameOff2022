using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject[] bossSoldiers;
    [SerializeField] private GameObject bossShield;

    [SerializeField] private Text bossHealth;
    [SerializeField] private GameObject bossSlimeAttack;
    [SerializeField] private GameObject enemyHealer;

    float maxX = 39;
    float maxY = 39;
    int nextAttack = 0;
    float startAttackRate = 5f;
    float attackRate;

    string[] bossAttacks = { "BossAttack1", "BossAttack2", "BossAttack3" };
    string[] bossGrowls = { "BossGrowl1", "BossGrowl2", "BossGrowl3" };

    bool isAttacking = false;
    bool isBossAttack1 = false;
    
    void Start()
    {
        nextAttack = 0;
        attackRate = startAttackRate;
        isAttacking = false;
        isBossAttack1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        AliveEnemies();

        UpdateBossHealth();

        if (isAttacking)
            return;

        if(attackRate <= 0f)
		{
            Invoke(bossAttacks[nextAttack],0f);
		}

        attackRate -= Time.deltaTime;
    }

    void BossAttack2()
	{
        FindObjectOfType<AudioManager>().Play(bossGrowls[Random.Range(0,bossGrowls.Length)]);
        isAttacking = true;
        isBossAttack1 = true;
        bossShield.SetActive(true);
        gameObject.GetComponent<EnemyHealth>().bossShield = true;
        attackRate = startAttackRate;

        int randomAmount = Random.Range(6,10);

		for (int i = 0; i < randomAmount; i++)
		{
            float randomX = Random.Range(-maxX, maxX);
            float randomY = Random.Range(-maxY, maxY);
            Vector3 spawnPosition = new Vector3(Mathf.Sign(randomX) * (Mathf.Clamp(Mathf.Abs(randomX),10f,39f)), Mathf.Sign(randomY) * (Mathf.Clamp(Mathf.Abs(randomY), 10f, 39f)), 0f);
            Instantiate(bossSoldiers[Random.Range(0,bossSoldiers.Length)],spawnPosition,Quaternion.identity);
		}

        nextAttack = 2;
	}

    void BossAttack1()
	{
        FindObjectOfType<AudioManager>().Play(bossGrowls[Random.Range(0, bossGrowls.Length)]);
        isAttacking = true;
        attackRate = startAttackRate;
        float rotationZ = 0f;
		for (int i = 0; i < 8; i++)
		{
            Instantiate(bossSlimeAttack,transform.position,Quaternion.Euler(0f,0f,rotationZ));
            rotationZ += 5f;
		}

        nextAttack = 1;
        StartCoroutine(SetIsAttacking(false));
	}

    void BossAttack3()
	{
        FindObjectOfType<AudioManager>().Play(bossGrowls[Random.Range(0, bossGrowls.Length)]);
        isAttacking = true;
        isBossAttack1 = true;
        bossShield.SetActive(true);
        gameObject.GetComponent<EnemyHealth>().bossShield = true;
        attackRate = startAttackRate;

        int randomAmount = Random.Range(3, 5);

        for (int i = 0; i < randomAmount; i++)
        {
            float randomX = Random.Range(-maxX, maxX);
            float randomY = Random.Range(-maxY, maxY);
            Vector3 spawnPosition = new Vector3(Mathf.Sign(randomX) * (Mathf.Clamp(Mathf.Abs(randomX), 10f, 39f)), Mathf.Sign(randomY) * (Mathf.Clamp(Mathf.Abs(randomY), 10f, 39f)), 0f);
            Instantiate(enemyHealer, spawnPosition, Quaternion.identity); 
        }

        nextAttack = 0; 
	}


    void UpdateBossHealth()
	{
        bossHealth.text = gameObject.GetComponent<EnemyHealth>().GetEnemyHealth().ToString();
	}

    void AliveEnemies()
	{
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(isBossAttack1 && aliveEnemies.Length <= 0)
		{
            isAttacking = false;
            isBossAttack1 = false;
            bossShield.SetActive(false);
            gameObject.GetComponent<EnemyHealth>().bossShield = false;
        }
    }

    IEnumerator SetIsAttacking(bool trueFalse)
    {
        yield return new WaitForSeconds(2f);
        isAttacking = trueFalse;
    }

}
