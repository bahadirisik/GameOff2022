using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    private Transform player;
    bool canInteract = false;
    void Start()
    {
		if (FindObjectOfType<GameManager>().isPlayerDead)
		{
            return;
		}

        player = FindObjectOfType<PlayerHealth>().transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().EnableInteract();
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<PlayerHealth>().DisableInteract();
            canInteract = false;
        }
    }

    void Update()
    {
       if (canInteract && Input.GetKeyDown(KeyCode.E))
       {
          player.gameObject.GetComponent<PlayerHealth>().DisableInteract();
          TakeThePortal();
       }
    }

    void TakeThePortal()
    {
		if (FindObjectOfType<GameManager>().isBossLevel)
		{
            PlayerStats.currentBuildIndex = 7;
		}

        FindObjectOfType<SceneChanger>().LoadSceneByBuildIndex(PlayerStats.currentBuildIndex);
    }
}
