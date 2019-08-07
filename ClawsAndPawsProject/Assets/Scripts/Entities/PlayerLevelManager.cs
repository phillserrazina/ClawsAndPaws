using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLevelManager : MonoBehaviour {

	public static PlayerLevelManager instance;

	private static int currentPlayerLevel;
	public static int availableAttributePoints;
	private static bool awaitingLevelUp = false;

	private static Actor player;

	public void Initialize() {
		Singleton();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

		currentPlayerLevel = player.characterData.level;
	}

	private void Singleton() {
		if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(gameObject);
			instance = this;
		}

		DontDestroyOnLoad(gameObject);
	}

	public static bool CheckLevel() {
		int curLvl = Mathf.FloorToInt(0.1f * Mathf.Sqrt(player.characterData.experiencePoints)) + 1;
		
		if (curLvl != currentPlayerLevel) {
			if (awaitingLevelUp == false) {
				availableAttributePoints = 5;
				awaitingLevelUp = true;
			}
			return true;
		}

		return false;
	}

	public static void UpdateLevel() {
		int curLvl = Mathf.FloorToInt(0.1f * Mathf.Sqrt(player.characterData.experiencePoints)) + 1;

		FindObjectOfType<CurrentCharacterManager>().currentCharacter.level = curLvl;
		awaitingLevelUp = false;
	}
}
