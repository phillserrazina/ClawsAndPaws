using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour {

	// VARIABLES

	public CharacterSO characterData;
	public string actorName { get; private set; }

	public Actor opponent;

	public int level { get; private set; }

	public Attributes attributes { get; private set; }
	public Stats stats { get; private set; }
	public Combat combat { get; private set; }
	public Appearance appearance { get; private set; }

	// METHODS

	public void Initialize() {

		characterData = gameObject.tag.Equals("Player") ? 
							FindObjectOfType<CurrentCharacterManager>().currentCharacter :
							FindObjectOfType<CurrentCharacterManager>().currentOpponent;

		actorName = characterData.actorName;
		opponent = GetOpponent();

		attributes = GetComponent<Attributes>();
		stats = GetComponent<Stats>();
		combat = GetComponent<Combat>();
		appearance = GetComponent<Appearance>();

		attributes.Initialize();
		stats.Initialize();
		combat.Initialize();
		appearance.Initialize();

		if (transform.position.x > opponent.transform.position.x) {
			transform.rotation = Quaternion.LookRotation(Vector3.back);
		}
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
