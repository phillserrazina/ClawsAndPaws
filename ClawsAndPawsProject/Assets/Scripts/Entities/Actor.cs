using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// VARIABLES

	public CharacterSO characterData;
	public string actorName { get; private set; }

	public float experiencePoints { get; private set; }

	public int level {
		get {
			return Mathf.FloorToInt((experiencePoints + 1000) / 1000);
		}
	}

	public Actor opponent;

	public Stats stats { get; private set; }
	public Combat combat { get; private set; }
	public Attributes attributes { get; private set; }

	// METHODS

	public void Initialize() {

		characterData = gameObject.tag.Equals("Player") ? 
							FindObjectOfType<CurrentCharacterManager>().currentCharacter :
							FindObjectOfType<CurrentCharacterManager>().currentOpponent;

		actorName = characterData.actorName;
		opponent = GetOpponent();

		experiencePoints = characterData.experiencePoints;

		stats = GetComponent<Stats>();
		combat = GetComponent<Combat>();
		attributes = GetComponent<Attributes>();

		attributes.Initialize();
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
