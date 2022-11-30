using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //private int startPlayerHealth = 100;
    [SerializeField] private GameObject interactKey;

    public int playerHealth { get; private set; }

    private float startIncreaseHealthTimer = 5f;
    private float increaseHealthTimer = 0f;

    private float startIncreaseTimer = 0.1f;
    private float increaseTimer;

    private float startPlayerDamageTimer = 1f;
    private float playerDamageTimer;

    [SerializeField] private GameManager gameManager;
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private GameObject playerDeathEffect;

    private bool isPlayerDead = false;

	private void Awake()
	{
        playerHealth = PlayerStats.startPlayerHealth;
    }
	void Start()
    {
        isPlayerDead = false;
        playerDamageTimer = startPlayerDamageTimer;
        increaseTimer = startIncreaseTimer;
        increaseHealthTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead)
            return;

        if(playerHealth <= 0)
		{
            playerDeath();
		}

        if(increaseHealthTimer <= 0f && increaseTimer <= 0f && playerHealth < PlayerStats.startPlayerHealth)
		{
            IncreasePlayerHealth();
		}

        playerDamageTimer -= Time.deltaTime;
        increaseHealthTimer -= Time.deltaTime;
        increaseTimer -= Time.deltaTime;
    }

    public void PlayerGetsDamage(int damage)
	{
        if(playerDamageTimer > 0f)
		{
            return;
		}
        CameraShake.instance.ShakeCamera(8f,0.2f);
        FindObjectOfType<AudioManager>().Play("PlayerHurt");
        playerDamageTimer = startPlayerDamageTimer;
        increaseHealthTimer = startIncreaseHealthTimer;
        playerHealth -= damage;
	}

    private void IncreasePlayerHealth()
	{
        increaseTimer = startIncreaseTimer;
        playerHealth += 1;
        if(playerHealth >= PlayerStats.startPlayerHealth)
		{
            playerHealth = PlayerStats.startPlayerHealth;
		}
        gameManager.UptadeHealth();
	}

    private void playerDeath()
	{
        isPlayerDead = true;
        gameManager.PlayerDeathUpdate();
        gameManager.GetComponent<WaveSpawner>().StopSpawnEnemy();
        Instantiate(playerDeathEffect,transform.position,Quaternion.identity);
        FindObjectOfType<AudioManager>().Play("Killed");
        //gameManager.GetComponent<WaveSpawner>().StopSpawnEnemy();
        gameObject.SetActive(false);
        sceneChanger.GetComponent<SceneChanger>().LoadSceneByBuildIndex(8);
	}

    public void EnableInteract()
	{
        interactKey.SetActive(true);
	}

    public void DisableInteract()
    {
        interactKey.SetActive(false);
    }
}
