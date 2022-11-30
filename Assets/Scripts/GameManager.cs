using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text timerText;
    [SerializeField] private Text enemyKilledCountText;
    [SerializeField] private GameObject upgradeOrb;
    [SerializeField] private GameObject levelPortal;
    [SerializeField] private GameObject player;
    [SerializeField] private WaveSpawner waveSpawner;
    [SerializeField] private GameObject upgradeStatPanel;
    [SerializeField] private GameObject pauseMenu;

    public int orbDamageReducer = 50;

    private bool isGameFinished;
    private bool isClockTicking = false;
    private bool isBossDead;
    public bool isPlayerDead = false;
    public bool isBossLevel;

    [SerializeField] private float timer = 10f;
    private int enemyKilledCount = 0;

    void Start()
    {
        isPlayerDead = false;
        isBossDead = false;
        isGameFinished = false;
        enemyKilledCount = 0;
        playerHealthText.text = player.GetComponent<PlayerHealth>().playerHealth.ToString();
        enemyKilledCountText.text = enemyKilledCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
            PauseMenu();
		}

        if (isGameFinished)
            return;

		if (isBossLevel && !GameObject.FindGameObjectWithTag("Boss"))
		{
            SpawnLevelPortal();
		}

        if(timer <= 5f && !isClockTicking)
		{
            FindObjectOfType<AudioManager>().Play("Clock");
            isClockTicking = true;
        }

        if (timer <= 0f)
		{
            PlayerStats.currentBuildIndex++;
            SpawnUpgradeOrb();
            SpawnLevelPortal();
		}

        timer -= Time.deltaTime;

        timer = Mathf.Clamp(timer, 0f, Mathf.Infinity);

        timerText.text = string.Format("{0:00.00}", timer);
    }

    public void UptadeHealth()
	{
        playerHealthText.text = player.GetComponent<PlayerHealth>().playerHealth.ToString();
    }

    public void UpdateEnemyKilledCount()
	{
        if (timer <= 0)
            return;
        enemyKilledCount++;
        enemyKilledCountText.text = enemyKilledCount.ToString();
	}

    void SpawnUpgradeOrb()
	{
        waveSpawner.StopSpawnEnemy();
        isGameFinished = true;
        FindObjectOfType<AudioManager>().Play("Winning");
        Instantiate(upgradeOrb, new Vector3(15f,0f,0f), Quaternion.identity);
	}

    void SpawnLevelPortal()
	{
        isGameFinished = true;
        Instantiate(levelPortal, new Vector3(-15f, 0f, 0f), Quaternion.identity);
    }

    public int getEnemyKillCount()
	{
        return enemyKilledCount;
	}

    public void UpgradeStatPanel()
	{
        upgradeStatPanel.SetActive(true);
        Time.timeScale = 0f;
	}

    public void UpgradeStrength(int amount)
	{
        Time.timeScale = 1f;
        PlayerStats.playerStrength += amount;
        upgradeStatPanel.SetActive(false);
    }
    public void UpgradeSpeed(float amount)
    {
        Time.timeScale = 1f;
        PlayerStats.playerMoveSpeed += amount;
        upgradeStatPanel.SetActive(false);
    }
    public void UpgradeHealth(int amount)
    {
        Time.timeScale = 1f;
        PlayerStats.startPlayerHealth += amount;
        upgradeStatPanel.SetActive(false);
    }

    void PauseMenu()
	{
        pauseMenu.SetActive(!pauseMenu.activeSelf);

		if (pauseMenu.activeSelf)
		{
            Time.timeScale = 0f;
		}
        else if (!pauseMenu.activeSelf)
		{
            Time.timeScale = 1f;
		}
	}

    public void PlayerDeathUpdate()
	{
        isPlayerDead = true;
	}
}
