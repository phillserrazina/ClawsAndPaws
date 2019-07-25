using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Combat : MonoBehaviour {

	// VARIABLES

	public enum Actions {
		Attack,
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
			case Combat.Actions.Attack:
				actor.opponent.stats.TakeDamage(actor.stats.attackPoints);
				break;

			case Combat.Actions.Defend:
				isDefending = true;
				break;
			
			case Combat.Actions.Rest:
				actor.stats.RestoreHealth(5);
				break;

			default:
				Debug.LogError("Actor::ExecuteAction --- Invalid Action.");
				return;
		}
	}
}
