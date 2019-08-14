using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettings : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider generalSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider vfxSlider;

    private static float currentGeneralVolume;
    private static float currentMusicVolume;
    private static float currentVFXVolume;

    private static string filePath;

    // EXECUTION METHODS

    private void Awake() {
        filePath = Application.persistentDataPath + "/Config.json";
        audioMixer = FindObjectOfType<AudioManager>().audioMixer;

        InitData();
    }

    // METHODS

    public void RestoreDefaults() {
        currentGeneralVolume = 0f;
        currentMusicVolume = 0f;
        currentVFXVolume = 0f;

        generalSlider.value = 0f;
        musicSlider.value = 0f;
        vfxSlider.value = 0f;

        audioMixer.SetFloat("generalVolume", 0f);
        audioMixer.SetFloat("musicVolume", 0f);
        audioMixer.SetFloat("vfxVolume", 0f);

        Save(filePath);
    }

    public void SetGeneral(float val) {
        audioMixer.SetFloat("generalVolume", val);
        currentGeneralVolume = val;
        Save(filePath);
    }

    public void SetMusic(float val) {
        audioMixer.SetFloat("musicVolume", val);
        currentMusicVolume = val;
        Save(filePath);
    }

    public void SetVfx(float val) {
        audioMixer.SetFloat("vfxVolume", val);
        currentVFXVolume = val;
        Save(filePath);
    }

    public static ConfigData CreateNewSettingsFile() {
		ConfigData configData = new ConfigData();
        configData.CreateDefault();

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
                configData = CreateNewSettingsFile();
        }

        configData.audioSettingsData.generalVolume = currentGeneralVolume;
        configData.audioSettingsData.musicVolume = currentMusicVolume;
        configData.audioSettingsData.vfxVolume = currentVFXVolume;

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
            configData = CreateNewSettingsFile();
        
        AudioSettingsData data = configData.audioSettingsData;

        currentGeneralVolume = data.generalVolume;
        currentMusicVolume = data.musicVolume;
        currentVFXVolume = data.vfxVolume;

        generalSlider.value = currentGeneralVolume;
        musicSlider.value = currentMusicVolume;
        vfxSlider.value = currentVFXVolume;
	}
}
