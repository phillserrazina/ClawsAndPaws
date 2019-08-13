using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private void Awake()  {		
		string filePath = Application.persistentDataPath + "/" + "Config.json";

		if (System.IO.File.Exists(filePath) == false) {
			System.IO.File.Create(filePath).Close();
			VideoSettings.CreateNewVideoSettingsFile();
		}

		VideoSettingsData data = VideoSettings.ReadData(filePath).videoSettingsData;

		Screen.fullScreen = data.isFullscreen;
		Screen.SetResolution(data.currentResolutionWidth, data.currentResolutionHeight, data.isFullscreen);
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
