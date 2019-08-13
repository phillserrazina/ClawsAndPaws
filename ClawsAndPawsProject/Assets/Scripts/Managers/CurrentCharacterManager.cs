using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
		if (SceneManager.GetActiveScene().name == "MainMenu") Destroy(gameObject);
	}

	public void Initialize() {
		Singleton();

		if (currentCharacter == null) {
			CharacterData newData = new CharacterData();
			newData.CreateDefault();
			currentCharacter = newData.GetSO();
		}

		if (SceneManager.GetActiveScene().name == "FightScene") {
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
