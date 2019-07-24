using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// VARIABLES

	public enum Actions {
		Attack,
		Defend,
		Skip
	}

	public CharacterSO characterData;
	private Actions currentChoice;
	public void SetCurrentChoice(Actions choice) { currentChoice = choice; }

	private float maxHealthPoints;
	private float currentHealthPoints;

	private float maxStaminaPoints;
	private float currentStaminaPoints;

	public float speedPoints { get; private set; }

	// EXECUTION METHODS

	private void Start() {
		Initialize();
	}

	// METHODS

	protected void Initialize() {
		maxHealthPoints = characterData.healthPoints;
		maxStaminaPoints = characterData.staminaPoints;
		speedPoints = characterData.speedPoints;

		currentHealthPoints = maxHealthPoints;
		currentStaminaPoints = maxStaminaPoints;
	}

	public void TakeDamage(float damage) {
		currentHealthPoints -= damage;
	}

	public void ExecuteAction(Actions action) {
		switch (action)
		{
			case Actions.Attack:
				break;

			case Actions.Defend:
				break;
			
			case Actions.Skip:
				break;

			default:
				Debug.LogError("Actor::ExecuteAction --- Invalid Action.");
				return;
		}
	}
}
