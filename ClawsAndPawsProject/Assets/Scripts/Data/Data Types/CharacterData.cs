using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

	public string characterName;
	
	public float strengthPoints;
	public float agilityPoints;
	public float healthPoints;
	public float staminaPoints;
	public float intimidationPoints;

	public void Create(CharacterSO data) {
		characterName = data.actorName;

		strengthPoints = data.strengthPoints;
		agilityPoints = data.agilityPoints;
		healthPoints = data.healthPoints;
		staminaPoints = data.staminaPoints;
		intimidationPoints = data.intimidationPoints;
	}

	public void CreateDefault() {
		characterName = "Default";

		strengthPoints = 1;
		agilityPoints = 1;
		healthPoints = 1;
		staminaPoints = 1;
		intimidationPoints = 1;
	}
}
