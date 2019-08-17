using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	private void Awake()  {		
		string filePath = Application.persistentDataPath + "/" + "Config.json";

		if (System.IO.File.Exists(filePath) == false) {
			System.IO.File.Create(filePath).Close();
			VideoSettings.CreateNewSettingsFile();
		}

		VideoSettingsData videoData = VideoSettings.ReadData(filePath).videoSettingsData;

		Screen.fullScreen = videoData.isFullscreen;
		Screen.SetResolution(videoData.currentResolutionWidth, videoData.currentResolutionHeight, videoData.isFullscreen);
	}

	private void Start() {
		string filePath = Application.persistentDataPath + "/" + "Config.json";

		if (System.IO.File.Exists(filePath) == false) {
			System.IO.File.Create(filePath).Close();
			AudioSettings.CreateNewSettingsFile();
		}

		AudioSettingsData audioData = AudioSettings.ReadData(filePath).audioSettingsData;

		AudioManager audioManager = FindObjectOfType<AudioManager>();
		audioManager.audioMixer.SetFloat("generalVolume", audioData.generalVolume);
		audioManager.audioMixer.SetFloat("musicVolume", audioData.musicVolume);
		audioManager.audioMixer.SetFloat("vfxVolume", audioData.vfxVolume);

		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
	}

	public void LoadScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void ExitGame() {
		Application.Quit();
	}
}
