using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	// EXECUTION METHODS

	// METHODS

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void ReloadCurrentScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
