using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// VARIABLES

	public CharacterSO characterData;

	public Actor opponent;

	public Stats stats { get; private set; }
	public Combat combat { get; private set; }

	// EXECUTION METHODS

	private void Awake() {
		Initialize();	// TODO: Put in a spawn manager of sorts?
	}

	// METHODS

	protected void Initialize() {
		opponent = GetOpponent();

		stats = GetComponent<Stats>();
		combat = GetComponent<Combat>();

		stats.Initialize();
		combat.Initialize();
	}

	private Actor GetOpponent() {
		foreach (Actor actor in FindObjectsOfType<Actor>()) {
			if (actor.gameObject != gameObject) {
				return actor;
			}
		}

		return null;
	}
}
