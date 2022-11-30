using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public Animator anim;

    public void LoadSceneByBuildIndex(int index)
	{
		StartCoroutine(LoadLevel(index));
	}

	public void LoadSceneByName(string name)
	{
		SceneManager.LoadScene(name);
	}

	IEnumerator LoadLevel(int buildIndex)
	{
		Time.timeScale = 1f;
		anim.SetTrigger("Start");

		yield return new WaitForSeconds(1f);


		SceneManager.LoadScene(buildIndex);
	}
}
