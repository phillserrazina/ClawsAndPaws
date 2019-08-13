using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VideoSettingsData
{
    public bool isFullscreen;
	public int currentResolutionWidth;
	public int currentResolutionHeight;

	public void CreateDefault() {
		isFullscreen = true;
		currentResolutionWidth = Screen.currentResolution.width;
		currentResolutionHeight = Screen.currentResolution.height;
	}
}
