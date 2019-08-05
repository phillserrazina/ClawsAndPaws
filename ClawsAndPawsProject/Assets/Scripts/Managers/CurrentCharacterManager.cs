using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCharacterManager : MonoBehaviour {

	public static CurrentCharacterManager instance;

	private void Singleton() {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(instance.gameObject);
			instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	public CharacterSO currentCharacter { get; private set; }
	public OpponentSO currentOpponent { get; private set; }

	public void SetCharacter(CharacterSO character) {
		currentCharacter = character;
	}

	public void SetOpponent(OpponentSO character) {
		currentOpponent = character;
	}

	public void Initialize() {
		Singleton();

		if (currentCharacter == null) {
			CharacterData newData = new CharacterData();
			newData.CreateDefault();
			currentCharacter = newData.GetSO();
		}

		if (currentOpponent == null) {
			SetRandomOpponent();
		}
	}

	public void SetRandomOpponent() {
		OpponentSO newOpponent = ScriptableObject.CreateInstance<OpponentSO>();
		newOpponent.CreateRandom(currentCharacter.experiencePoints);
		currentOpponent = newOpponent;
	}
}
