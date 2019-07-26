using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Combat : MonoBehaviour {

	// VARIABLES

	public enum Actions {
		Attack,
		Items,
		Defend,
		Rest
	}

	public Actions currentChoice { get; private set; }
	public void SetChoice(string choice) {
		 currentChoice = (Actions)System.Enum.Parse(typeof(Actions), choice);
	}

	public void SetChoice(Actions choice) {
		 currentChoice = choice;
	}

	public void SetRandomChoice() {
		SetChoice((Combat.Actions)Random.Range(0, 3));
	}

	public AttackSO currentAttack { get; private set; }

	public void SetAttack(AttackSO attackData) {
		currentAttack = attackData;
	}

	public void SetRandomAttack() {
		SetAttack(attackList.attacks[Random.Range(0, attackList.attacks.Length-1)]);
	}

	public AttackListSO attackList;
	
	public bool isDefending;

	private Actor actor;

	// METHODS

	public void Initialize() {
		actor = GetComponent<Actor>();
	}

	public void ExecuteAction() {
		print(gameObject.name + " used \"" + currentChoice.ToString() + "\"!");
		switch (currentChoice)
		{
			case Actions.Attack:
				ExecuteAttack();
				break;
			
			case Actions.Items:
				break;

			case Actions.Defend:
				isDefending = true;
				break;
			
			case Actions.Rest:
				RestAction();
				break;

			default:
				Debug.LogError("Actor::ExecuteAction --- Invalid Action.");
				return;
		}
	}

	private void ExecuteAttack() {
		if (actor.stats.currentStaminaPoints < currentAttack.staminaCost) {
			RestAction();
			return;
		}

		actor.stats.DepleteStamina(currentAttack.staminaCost);
		float damage = currentAttack.damagePoints + actor.stats.attackPoints;
		actor.opponent.stats.TakeDamage(damage);
	}

	private void SpecialAttackChoice() {
		float staminaValue = 30f;
		if (actor.stats.currentStaminaPoints < staminaValue) {
			RestAction();
			return;
		}

		actor.stats.DepleteStamina(staminaValue);
		actor.opponent.stats.TakeDamage(actor.stats.attackPoints*3);
	}

	private void RestAction() {
		actor.stats.RestoreHealth(5);
		actor.stats.RestoreStamina(10f);
	}
}
