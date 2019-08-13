using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    // VARIABLES
    [SerializeField] private Button resolutionButton;
    [SerializeField] private Toggle fullscreenToggle;

    private static bool isFullscreen;
    private static Resolution currentResolution;

    private Resolution[] resolutions;
	private static int currentResolutionIndex;
	private static int fullScreenResolutionIndex;

    private static string filePath;

    // EXECUTION METHODS

	private void Awake() {
        Initialize();
	}

    // METHODS

    private void Initialize() {
        filePath = Application.persistentDataPath + "/" + "Config.json";

		if (System.IO.File.Exists(filePath) == false) {
			System.IO.File.Create(filePath).Close();
			CreateNewVideoSettingsFile();
		}

        InitData();

        resolutions = Screen.resolutions;

		for (int i = 0; i < resolutions.Length; i++) {
			if (resolutions[i].width == currentResolution.width &&
				resolutions[i].height == currentResolution.height) {
				currentResolutionIndex = i;
				fullScreenResolutionIndex = i;
			}
		}

		fullscreenToggle.isOn = isFullscreen;
		resolutionButton.GetComponentInChildren<Text>().text = currentResolution.width + "x" + currentResolution.height;
    }

    public void SetFullscreen() {
		isFullscreen = fullscreenToggle.isOn;

		if (isFullscreen) {
			currentResolution.width = Screen.currentResolution.width;
			currentResolution.height = Screen.currentResolution.height;
			resolutionButton.GetComponentInChildren<Text>().text = currentResolution.width + "x" + currentResolution.height;
			currentResolutionIndex = fullScreenResolutionIndex;
		}

        resolutionButton.interactable = !isFullscreen;
        Screen.fullScreen = isFullscreen;
        Save(filePath);
	}

	public void SetResolution() {
		currentResolutionIndex++;

		if (currentResolutionIndex >= resolutions.Length) 
			currentResolutionIndex = 0;
		
		currentResolution = resolutions[currentResolutionIndex];
		resolutionButton.GetComponentInChildren<Text>().text = currentResolution.width + "x" + currentResolution.height;

		Screen.SetResolution(currentResolution.width, currentResolution.height, isFullscreen);
        Save(filePath);
	}

	public static ConfigData CreateNewVideoSettingsFile() {
		ConfigData configData = new ConfigData();
        configData.videoSettingsData.CreateDefault();

		Save(filePath, configData);
        return configData;
	}

    private static void Save(string path, ConfigData configData=null) {

        if (System.IO.File.Exists(filePath) == false) {
			Debug.LogError("Unable to read file at " + filePath + "; File does not exist");
			return;
		}

        if (configData == null) {
            configData = ReadData(filePath);
        
            if (configData == null)
                configData = CreateNewVideoSettingsFile();
        }

        configData.videoSettingsData.currentResolutionWidth = currentResolution.width;
        configData.videoSettingsData.currentResolutionHeight = currentResolution.height;
        configData.videoSettingsData.isFullscreen = isFullscreen;

		// Form contents, encrypt them and write them to the file
		string contents = JsonUtility.ToJson(configData, true);
		System.IO.File.WriteAllText (filePath, contents);
    }

    public static ConfigData ReadData(string path) {
		// Get file path
		string filePath = path;

		if (System.IO.File.Exists(filePath) == false) {
			Debug.LogError("Unable to read file at " + filePath + "; File does not exist");
			return null;
		}

		// Get file and decrypt them contents
		string contents = System.IO.File.ReadAllText(filePath);

		// Get game data from retrieved file
		ConfigData data = JsonUtility.FromJson<ConfigData>(contents);

		if (data == null) {
			Debug.LogError("File at " + filePath + " is corrupted! No file was found");
			return null;
		}

		return data;
	}

    private void InitData() {
		ConfigData configData = ReadData(filePath);

        if (configData == null)
            configData = CreateNewVideoSettingsFile();
        
        VideoSettingsData data = configData.videoSettingsData;

		isFullscreen = data.isFullscreen;

		currentResolution = new Resolution();
		currentResolution.width = data.currentResolutionWidth;
		currentResolution.height = data.currentResolutionHeight;
	}
}
