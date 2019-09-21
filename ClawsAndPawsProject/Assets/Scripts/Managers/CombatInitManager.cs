using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatInitManager : MonoBehaviour {

	private void Start() {

		FindObjectOfType<CurrentCharacterManager>().Initialize();

		TournamentManager tManager = FindObjectOfType<TournamentManager>();
		if (tManager != null) tManager.Initialize();

		Actor player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

		player.Initialize(true);
		player.opponent.Initialize(true);

		Inventory.instance.Initialize();
		FindObjectOfType<PlayerLevelManager>().Initialize();
		
		FindObjectOfType<TurnManager>().Initialize();
		FindObjectOfType<UIManager>().Initialize();
	}
}
