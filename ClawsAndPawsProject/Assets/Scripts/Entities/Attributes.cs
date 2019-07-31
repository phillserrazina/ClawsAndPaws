using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {

	// VARIABLES

	public float strengthPoints { get; private set; }
	public float agilityPoints { get; private set; }
	public float healthPoints { get; private set; }
	public float staminaPoints { get; private set; }
	public float intimidationPoints { get; private set; }

	private Actor character;

	// METHODS

	public void IncreaseStrength(float value) { strengthPoints += value; }
	public void IncreaseAgility(float value) { agilityPoints += value; }
	public void IncreaseHealth(float value) { healthPoints += value; }
	public void IncreaseStamina(float value) { staminaPoints += value; }
	public void IncreaseIntimidation(float value) { intimidationPoints += value; }

	public void Initialize() {
		character = GetComponent<Actor>();

		strengthPoints = character.characterData.strengthPoints;
		agilityPoints = character.characterData.agilityPoints;
		healthPoints = character.characterData.healthPoints;
		staminaPoints = character.characterData.staminaPoints;
		intimidationPoints = character.characterData.intimidationPoints;
	}
}
