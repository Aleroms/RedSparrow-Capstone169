using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private string _sceneName;
	[SerializeField]
	private bool _loadNextScene;
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.CompareTag("Player"))
		{
			if (_loadNextScene == true)
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
			else
			{
				SceneManager.LoadScene(_sceneName);
			}
		}
	}
	public void LoadNextLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
