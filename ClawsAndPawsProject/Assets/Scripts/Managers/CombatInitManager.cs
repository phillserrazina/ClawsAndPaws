using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInitManager : MonoBehaviour {

	private void Start() {

		FindObjectOfType<CurrentCharacterManager>().Initialize();

		Actor player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

		player.Initialize();
		player.opponent.Initialize();

		FindObjectOfType<Inventory>().Initialize();
		FindObjectOfType<PlayerLevelManager>().Initialize();
		
		FindObjectOfType<TurnManager>().Initialize();
		FindObjectOfType<UIManager>().Initialize();
	}
}
