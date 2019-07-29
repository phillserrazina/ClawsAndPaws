using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// VARIABLES

	[Header("UI Objects")]
	[SerializeField] private GameObject playerChoiceMenu;
	[SerializeField] private GameObject winnerWidget;

	[Header("Player Stats")]
	[SerializeField] private Text leftCharacterHealthText;
	[SerializeField] private Text leftCharacterStaminaText;

	[Space(10)]
	[SerializeField] private Text rightCharacterHealthText;
	[SerializeField] private Text rightCharacterStaminaText;

	[Header("Text")]
	[SerializeField] private Text winnerText;

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

	public void SetPlayerAttack(AttackSO attackData) {
		player.combat.SetAttack(attackData);
	}

	public void TriggerWinnerWidget() {
		winnerText.text = string.Format("{0} Wins!", turnManager.winner.actorName);
		winnerWidget.SetActive(true);
	}

	public void UseItem(ItemSO itemData) {
		FindObjectOfType<Inventory>().UseItem(itemData);
		turnManager.NextState();
		playerChoiceMenu.SetActive(false);
	}
}
