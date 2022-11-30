using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int startPlayerHealth = 100;
    public static float playerMoveSpeed = 5f;
    public static int playerStrength = 5;
    public static int playerShield = 10;
	public static int currentBuildIndex = 2;

	private void Update()
	{
		if (playerMoveSpeed <= 1f)
			playerMoveSpeed = 1f;
		if (startPlayerHealth <= 20)
			startPlayerHealth = 20;
		if (playerStrength <= 0)
			playerStrength = 0;
	}

	public void RestartStats()
	{
		startPlayerHealth = 100;
		playerMoveSpeed = 5f;
		playerStrength = 5;
		playerShield = 10;
		currentBuildIndex = 2;
	}
}
