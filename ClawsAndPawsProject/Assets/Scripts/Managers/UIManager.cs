using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

	// VARIABLES

	public GameObject playerChoiceMenu;

	private Actor player;

	// EXECUTION METHODS

	private void Start() {
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		player = go.GetComponent<Actor>();
	}

	// METHODS

	public void TriggerPlayerChoiceMenu() {
		playerChoiceMenu.SetActive(true);
	}

	public void SetPlayerAction(string choice) {
		switch (choice)
		{
			
			
			default:
				Debug.LogError("UIManager::SetPlayerAction -- Invalid Choice.");
				return;
		}
	}
}
