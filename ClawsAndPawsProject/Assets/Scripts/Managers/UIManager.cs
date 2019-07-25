using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// VARIABLES

	[Header("UI Objects")]
	public GameObject playerChoiceMenu;

	[Header("Player Stats")]
	public Text leftCharacterHealthText;
	public Text leftCharacterStaminaText;

	[Space(10)]
	public Text rightCharacterHealthText;
	public Text rightCharacterStaminaText;

	private Actor player;
	private Actor cpu;

	private TurnManager turnManager;

	// EXECUTION METHODS

	private void Start() {
		Initialize();
	}

	// METHODS

	private void Initialize() {
		turnManager = FindObjectOfType<TurnManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
		cpu = player.opponent;
	}

	public void UpdateUI() {
		UpdatePlayerStats();
	}

	private void UpdatePlayerStats() {
		leftCharacterHealthText.text = player.stats.currentHealthPoints.ToString();
		leftCharacterStaminaText.text = player.stats.currentStaminaPoints.ToString();

		rightCharacterHealthText.text = cpu.stats.currentHealthPoints.ToString();
		rightCharacterStaminaText.text = cpu.stats.currentStaminaPoints.ToString();
	}

	public void TriggerPlayerChoiceMenu() {
		playerChoiceMenu.SetActive(true);
	}

	public void SetPlayerAction(string choice) {
		player.combat.SetChoice(choice);
		turnManager.NextState();
		playerChoiceMenu.SetActive(false);
	}
}
