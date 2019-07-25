using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Stats : MonoBehaviour {

	// VARIABLES

	private float maxHealthPoints;
	public float currentHealthPoints { get; private set; }

	private float maxStaminaPoints;
	public float currentStaminaPoints { get; private set; }

	public float speedPoints;
	public float attackPoints;

	private Actor actor;

	// METHODS

	public void Initialize() {
		actor = GetComponent<Actor>();

		maxHealthPoints = actor.characterData.healthPoints;
		maxStaminaPoints = actor.characterData.staminaPoints;
		speedPoints = actor.characterData.speedPoints;
		attackPoints = actor.characterData.attackPoints;

		currentHealthPoints = maxHealthPoints;
		currentStaminaPoints = maxStaminaPoints;
	}

	public void TakeDamage(float damage) {
		if (actor.combat.isDefending) {
			damage /= 2;
			actor.combat.isDefending = false;
		}

		currentHealthPoints -= damage;
	}

	public void RestoreHealth(float value) {
		if (currentHealthPoints >= maxHealthPoints) return;
		currentHealthPoints += value;
	}
}
