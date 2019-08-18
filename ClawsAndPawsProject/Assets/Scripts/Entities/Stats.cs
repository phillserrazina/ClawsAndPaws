﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Stats : MonoBehaviour {

	// VARIABLES

	private float maxHealthPoints;
	public float currentHealthPoints { get; private set; }
	public float healthDecimalPercentage { get { return currentHealthPoints / maxHealthPoints; } }

	private float maxStaminaPoints;
	public float currentStaminaPoints { get; private set; }
	public float staminaDecimalPercentage { get { return currentStaminaPoints / maxStaminaPoints; } }

	private float sp = 0;
	public float speedPoints { get { return GetComponent<Attributes>().agilityPoints + sp; } set { sp = value; } }
	private float ap = 0;
	public float attackPoints { get { return GetComponent<Attributes>().strengthPoints + ap; } set { ap = value; } }
	private float dp = 0;
	public float defensePoints { get { return 1 + dp; } set { dp = value; dp = Mathf.Clamp(dp, 0, 9); } }

	private Stack<ConditionSO> currentConditions = new Stack<ConditionSO>();

	private Actor actor;

	// METHODS

	public void Initialize() {
		actor = GetComponent<Actor>();

		maxHealthPoints = 90 + (actor.characterData.healthPoints * 10);
		maxStaminaPoints = 90 + (actor.characterData.staminaPoints * 10);

		currentHealthPoints = maxHealthPoints;
		currentStaminaPoints = maxStaminaPoints;
	}

	public void TakeDamage(float damage, bool trueDamage=false) {
		Debug.Log(actor.characterData.actorName + " taking " + damage + "damage...");
		if (currentHealthPoints <= 0) return;

		if (actor.combat.isDefending) {
			damage /= 2;
			actor.combat.isDefending = false;
		}

		damage /= Mathf.Log(defensePoints*10, 10);
		currentHealthPoints -= damage;
		Debug.Log(actor.characterData.actorName + " ended up taking " + damage + "damage after defenses...");
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

		while (newStack.Count > 0) {
			ConditionSO c = newStack.Pop();
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
				defensePoints += cond.strength;
				break;
			
			case ConditionSO.Conditions.Reduce_Defense:
				defensePoints -= cond.strength;
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
