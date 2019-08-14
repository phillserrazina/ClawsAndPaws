using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioSettingsData
{
    public float generalVolume;
    public float vfxVolume;
    public float musicVolume;

	public void CreateDefault() {
		generalVolume = 0f;
        vfxVolume = 0f;
        musicVolume = 0f;
	}
}
