using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCharacterManager : MonoBehaviour {

	public static CurrentCharacterManager instance;

	private void Singleton() {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(gameObject);
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

	private void Awake() {
		if (FindObjectOfType<CombatInitManager>() != null) return;

		Singleton();
	}

	private void Update() {
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
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
		int level = currentCharacter.level;
		newOpponent.CreateRandom(level);
		currentOpponent = newOpponent;
	}
}
