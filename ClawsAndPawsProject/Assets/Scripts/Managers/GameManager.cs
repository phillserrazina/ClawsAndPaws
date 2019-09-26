using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	public bool isSpedUp { get; private set; }

	// EXECUTION METHODS

	private void Start() {
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		isSpedUp = IntToBool(PlayerPrefs.GetInt("Speed", 0));
		Time.timeScale = isSpedUp ? 5f : 1f;
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

		PlayerPrefs.SetInt("Speed", BoolToInt(isSpedUp));
	}

	private bool IntToBool(int i) {
		if (i == 1)
			return true;
		
		return false;
	}

	private int BoolToInt(bool b) {
		if (b)
			return 1;
		
		return 0;
	}
}
