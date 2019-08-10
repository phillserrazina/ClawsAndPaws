using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	// VARIABLES

	[Header("UI Objects")]
	[SerializeField] private GameObject playerChoiceMenu;
	[SerializeField] private GameObject winnerWidget;
	[SerializeField] private GameObject loserWidget;
	[SerializeField] private GameObject levelUpText;
	[SerializeField] private GameObject regularButton;
	[SerializeField] private GameObject levelUpButton;

	[Header("Player Stats")]
	[SerializeField] private Text leftCharacterName;
	[SerializeField] private Text leftCharacterLevel;

	[Space(10)]
	[SerializeField] private Image leftCharacterHealthGraphic;
	[SerializeField] private Image leftCharacterStaminaGraphic;

	[Space(20)]
	[SerializeField] private Text rightCharacterName;
	[SerializeField] private Text rightCharacterLevel;

	[Space(10)]
	[SerializeField] private Image rightCharacterHealthGraphic;
	[SerializeField] private Image rightCharacterStaminaGraphic;

	[Header("Text")]
	[SerializeField] private Text winnerText;
	[SerializeField] private Text loserText;
	[SerializeField] private Text loserGoldText;

	private Actor player;
	private Actor cpu;

	private TurnManager turnManager;

	// METHODS

	public void Initialize() {
		turnManager = FindObjectOfType<TurnManager>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
		cpu = player.opponent;
	}

	public void UpdateUI() {
		UpdatePlayerStats();
	}

	private void UpdatePlayerStats() {
		leftCharacterName.text = player.actorName;
		leftCharacterLevel.text = "Level " + player.characterData.level.ToString();
		leftCharacterHealthGraphic.fillAmount = player.stats.healthDecimalPercentage;
		leftCharacterStaminaGraphic.fillAmount = player.stats.staminaDecimalPercentage;

		rightCharacterName.text = cpu.actorName;
		rightCharacterLevel.text = "Level " + cpu.characterData.level.ToString();
		rightCharacterHealthGraphic.fillAmount = cpu.stats.healthDecimalPercentage;
		rightCharacterStaminaGraphic.fillAmount = cpu.stats.staminaDecimalPercentage;
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
		SetPlayerAction("Attack");
		player.combat.SetAttack(attackData);
	}

	public void TriggerPlayerWinWidget(int goldWon, int xpWon, ItemSO[] itemsWon, bool lvlUp=false) {
		winnerText.text = string.Format("{0} Wins!", player.actorName);
		winnerWidget.SetActive(true);
		levelUpText.SetActive(lvlUp);
		levelUpButton.SetActive(lvlUp);
		regularButton.SetActive(!lvlUp);
	}

	public void TriggerLoserWinWidget(int goldLost) {
		loserText.text = string.Format("{0} Wins!", turnManager.winner.actorName);
		loserGoldText.text = string.Format("{0} lost {1} gold...", player, goldLost);
		loserWidget.SetActive(true);
	}

	public void UseItem(ItemSO itemData) {
		SetPlayerAction("Items");
		player.combat.SetItem((ConsumableSO)itemData);
	}
}
