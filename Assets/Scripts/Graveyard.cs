using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graveyard : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private SceneChanger sceneChanger;
	bool canInteract = false; 
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

	private void Awake()
	{
		FindObjectOfType<AudioManager>().Play("Graveyard");
		Time.timeScale = 0f;
	}

	private void Update()
	{

		if (canInteract && Input.GetKeyDown(KeyCode.E))
		{
			sceneChanger.LoadSceneByBuildIndex(PlayerStats.currentBuildIndex);
		}
	}
}
