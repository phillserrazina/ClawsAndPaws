using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

	public string characterName;
	public int characterExperiencePoints;
	
	public int strengthPoints;
	public int agilityPoints;
	public int healthPoints;
	public int staminaPoints;
	public int intimidationPoints;

	public void Create(CharacterSO data) {
		characterName = data.actorName;
		characterExperiencePoints = data.experiencePoints;

		strengthPoints = data.strengthPoints;
		agilityPoints = data.agilityPoints;
		healthPoints = data.healthPoints;
		staminaPoints = data.staminaPoints;
		intimidationPoints = data.intimidationPoints;
	}

	public void CreateDefault() {
		characterName = "Default";
		characterExperiencePoints = 0;

		strengthPoints = 1;
		agilityPoints = 1;
		healthPoints = 1;
		staminaPoints = 1;
		intimidationPoints = 1;
	}

	public void CreateRandom(int playerExperiencePoints) {
		TextAsset file = Resources.Load("RandomCatNames") as TextAsset;
		string[] nameList = file.text.Split(',');

		characterName = nameList[Random.Range(0, nameList.Length)];
		characterExperiencePoints = Random.Range(playerExperiencePoints-2000, playerExperiencePoints+2000);
		characterExperiencePoints = Mathf.Clamp(characterExperiencePoints, 0, 20000);

		strengthPoints = Random.Range(0, 10);
		agilityPoints = Random.Range(0, 10);
		healthPoints = Random.Range(0, 10);
		staminaPoints = Random.Range(0, 10);
		intimidationPoints = Random.Range(0, 10);
	}

	public CharacterSO GetSO() {
		CharacterSO character = ScriptableObject.CreateInstance<CharacterSO>();

		character.name = "Default";
		character.actorName = characterName;
		character.experiencePoints = characterExperiencePoints;

		character.strengthPoints = strengthPoints;
		character.agilityPoints = agilityPoints;
		character.healthPoints = healthPoints;
		character.staminaPoints = staminaPoints;
		character.intimidationPoints = intimidationPoints;

		return character;
	}
}
