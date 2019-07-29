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

	private Stack<ConditionSO> currentConditions = new Stack<ConditionSO>();

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

	public void TakeDamage(float damage, bool trueDamage=false) {
		if (currentHealthPoints <= 0) return;

		if (actor.combat.isDefending) {
			damage /= 2;
			actor.combat.isDefending = false;
		}

		currentHealthPoints -= damage;
		if (currentHealthPoints <= 0) currentHealthPoints = 0;
	}

	public void AddCondition(ConditionSO condition) {
		condition = ScriptableObject.Instantiate(condition);
		currentConditions.Push(condition);
	}

	public void ApplyConditions() {
		if (currentConditions.Count <= 0) return;

		var newStack = new Stack<ConditionSO>(currentConditions);
		currentConditions.Clear();

		print ("Hi");

		while (newStack.Count > 0) {
			ConditionSO c = newStack.Pop();
			print("Applying " + c.condition.ToString());
			ExecuteConditions(c);
		}
	}

	private void ExecuteConditions(ConditionSO cond) {
		switch (cond.condition)
		{
			case ConditionSO.Conditions.Poison:
				TakeDamage(cond.strength, true);
				break;
			
			case ConditionSO.Conditions.Sleep:
				// TODO
				break;
			
			case ConditionSO.Conditions.Increase_Defense:
				// TODO
				break;
			
			case ConditionSO.Conditions.Reduce_Defense:
				// TODO
				break;
			
			case ConditionSO.Conditions.Increase_Speed:
				speedPoints += cond.strength;
				break;
			
			case ConditionSO.Conditions.Reduce_Speed:
				speedPoints -= cond.strength;
				break;
			
			default:
				break;
		}

		cond.duration--;
		if (cond.duration > 0) currentConditions.Push(cond);
	}

	public void DepleteStamina(float value) {
		if (currentStaminaPoints < value) return;

		currentStaminaPoints -= value;
		if (currentStaminaPoints <= 0) currentStaminaPoints = 0;
	}

	public void RestoreHealth(float value) {
		if (currentHealthPoints >= maxHealthPoints) return;
		currentHealthPoints += value;
	}

	public void RestoreStamina(float value) {
		if (currentStaminaPoints >= maxStaminaPoints) return;
		currentStaminaPoints += value;
	}
}
