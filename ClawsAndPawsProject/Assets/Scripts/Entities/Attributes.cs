using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {

	// VARIABLES

	public int strengthPoints { get; private set; }
	public int agilityPoints { get; private set; }
	public int healthPoints { get; private set; }
	public int staminaPoints { get; private set; }
	public int intimidationPoints { get; private set; }

	private Actor character;

	// METHODS

	public void IncreaseStrength(int value) { strengthPoints += value; }
	public void IncreaseAgility(int value) { agilityPoints += value; }
	public void IncreaseHealth(int value) { healthPoints += value; }
	public void IncreaseStamina(int value) { staminaPoints += value; }
	public void IncreaseIntimidation(int value) { intimidationPoints += value; }

	public void Initialize() {
		character = GetComponent<Actor>();

		strengthPoints = character.characterData.strengthPoints;
		agilityPoints = character.characterData.agilityPoints;
		healthPoints = character.characterData.healthPoints;
		staminaPoints = character.characterData.staminaPoints;
		intimidationPoints = character.characterData.intimidationPoints;
	}
}
