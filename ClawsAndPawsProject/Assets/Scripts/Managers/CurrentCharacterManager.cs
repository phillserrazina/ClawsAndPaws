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
	public CharacterSO currentOpponent { get; private set; }

	public void SetCharacter(CharacterSO character) {
		currentCharacter = character;
	}

	public void SetOpponent(CharacterSO character) {
		currentOpponent = character;
	}

	private void Awake() {
		Singleton();
	}

	public void Initialize() {
		if (currentCharacter == null) {
			CharacterData newData = new CharacterData();
			newData.CreateDefault();
			currentCharacter = newData.GetSO();
		}

		if (currentOpponent == null) {
			CharacterData newData = new CharacterData();
			newData.CreateDefault();
			currentOpponent = newData.GetSO();
		}
	}
}
