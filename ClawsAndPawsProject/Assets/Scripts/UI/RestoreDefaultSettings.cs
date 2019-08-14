using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreDefaultSettings : MonoBehaviour
{
    private AudioSettings audioSettings;
    private VideoSettings videoSettings;

    private void Start() {
        audioSettings = FindObjectOfType<AudioSettings>();
        videoSettings = FindObjectOfType<VideoSettings>();
    }

    public void RestoreDefault() {
        audioSettings.RestoreDefaults();
        videoSettings.RestoreDefaults();
    }
}
