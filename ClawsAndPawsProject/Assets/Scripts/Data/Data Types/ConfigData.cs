using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConfigData
{
    public VideoSettingsData videoSettingsData = new VideoSettingsData();
    public AudioSettingsData audioSettingsData = new AudioSettingsData();

    public void CreateDefault() {
        videoSettingsData.CreateDefault();
        audioSettingsData.CreateDefault();
    }
}
