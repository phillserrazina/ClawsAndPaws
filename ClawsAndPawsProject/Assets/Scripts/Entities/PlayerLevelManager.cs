using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLevelManager : MonoBehaviour {

	public static PlayerLevelManager instance;

	private static int currentPlayerLevel;
	public static int availableAttributePoints;

	private static Actor player;

	public void Initialize() {
		Singleton();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

		currentPlayerLevel = player.level;
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

	public static bool UpdateLevel() {
		int curLvl = Mathf.FloorToInt(0.1f * Mathf.Sqrt(player.experiencePoints)) + 1;
		
		if (curLvl != currentPlayerLevel) {
			availableAttributePoints = 5;
			return true;
		}

		return false;
	}
}
