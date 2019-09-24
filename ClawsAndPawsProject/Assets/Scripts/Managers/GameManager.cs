using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	private bool isSpedUp = false;

	// EXECUTION METHODS

	private void Start() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
	}

	// METHODS

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void ReloadCurrentScene() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MakeNewFight() {
		FindObjectOfType<CurrentCharacterManager>().SetRandomOpponent();
		ReloadCurrentScene();
	}

	public void SpeedUp() {
		isSpedUp = !isSpedUp;
		Time.timeScale = isSpedUp ? 5f : 1f;
	}
}
