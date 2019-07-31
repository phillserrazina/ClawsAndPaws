using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	// EXECUTION METHODS

	private void Start() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			LoadScene("MainMenu");
		}
	}

	// METHODS

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void ReloadCurrentScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
