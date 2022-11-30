using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private string songName;

	private void Start()
	{
        Time.timeScale = 1f;
        FindObjectOfType<AudioManager>().Play(songName);
    }

	public void PlayButton()
	{
        sceneChanger.LoadSceneByBuildIndex(1);
	}
    public void QuitButton()
    {
        Application.Quit();
    }

}
