using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// VARIABLES

	private float maxHealthPoints = 100f;
	private float currentHealthPoints;

	private float maxStaminaPoints = 100f;
	private float currentStaminaPoints;

	// EXECUTION METHODS

	private void Start() {
		Initialize();
	}

	// METHODS

	protected void Initialize() {
		currentHealthPoints = maxHealthPoints;
		currentStaminaPoints = maxStaminaPoints;
	}

	public void TakeDamage(float damage) {
		currentHealthPoints -= damage;
	}
}
