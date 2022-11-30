using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeOrb : MonoBehaviour
{
    private Transform player;
    [SerializeField] private int maxOrbDamage = 200;
    bool canInteract = false;
    int reducedOrbDamage;

    [SerializeField] private Text orbDamage;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.isPlayerDead)
            return;

        player = FindObjectOfType<PlayerHealth>().transform;
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "Player")
        {
            orbDamage.text = "Between 0 - " + (maxOrbDamage - (3 * FindObjectOfType<GameManager>().getEnemyKillCount()) - gameManager.orbDamageReducer).ToString() + " Damage";
            orbDamage.gameObject.SetActive(true);
            player.GetComponent<PlayerHealth>().EnableInteract();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            orbDamage.gameObject.SetActive(false);
            player.GetComponent<PlayerHealth>().DisableInteract();
            canInteract = false;
        }
    }

    void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            player.gameObject.GetComponent<PlayerHealth>().DisableInteract();
            TakeTheOrb();
        }
    }

    void TakeTheOrb()
	{
        reducedOrbDamage = maxOrbDamage - (3 * FindObjectOfType<GameManager>().getEnemyKillCount()) - gameManager.orbDamageReducer;
        if (reducedOrbDamage <= 0)
            reducedOrbDamage = 0;
        int randomOrbDamage = Random.Range(0, reducedOrbDamage);
        player.GetComponent<PlayerHealth>().PlayerGetsDamage(randomOrbDamage);
        FindObjectOfType<GameManager>().UptadeHealth();

        if(player.GetComponent<PlayerHealth>().playerHealth > 0)
		{
            FindObjectOfType<AudioManager>().Play("Orb");
            FindObjectOfType<GameManager>().UpgradeStatPanel();
            Destroy(gameObject);
		}
	}

    public void ReduceMaxOrbDamage(int quantity)
	{
        maxOrbDamage -= quantity;
	}
}
