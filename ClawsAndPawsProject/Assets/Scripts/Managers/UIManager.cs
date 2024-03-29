﻿using System.Collections;
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
	[SerializeField] private Text leftCharacterHealthText;
	[SerializeField] private Text leftCharacterStaminaText;

	[Space(20)]
	[SerializeField] private Text rightCharacterName;
	[SerializeField] private Text rightCharacterLevel;

	[Space(10)]
	[SerializeField] private Image rightCharacterHealthGraphic;
	[SerializeField] private Image rightCharacterStaminaGraphic;
	[SerializeField] private Text rightCharacterHealthText;
	[SerializeField] private Text rightCharacterStaminaText;

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
		leftCharacterHealthText.text = player.stats.currentHealthPoints.ToString("F0");
		leftCharacterStaminaText.text = player.stats.currentStaminaPoints.ToString("F0");

		rightCharacterName.text = cpu.actorName;
		rightCharacterLevel.text = "Level " + cpu.characterData.level.ToString();
		rightCharacterHealthGraphic.fillAmount = cpu.stats.healthDecimalPercentage;
		rightCharacterStaminaGraphic.fillAmount = cpu.stats.staminaDecimalPercentage;
		rightCharacterHealthText.text = cpu.stats.currentHealthPoints.ToString("F0");
		rightCharacterStaminaText.text = cpu.stats.currentStaminaPoints.ToString("F0");
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

	public void TriggerPlayerWinWidget(int goldWon, int xpWon, bool lvlUp=false) {
		winnerText.text = string.Format("{0} Wins!", player.actorName);
		winnerWidget.SetActive(true);
		levelUpText.SetActive(lvlUp);

		TournamentOpponentTracker tTracker = FindObjectOfType<TournamentOpponentTracker>();

		if (tTracker != null) {
			if ((tTracker.currentOpponentIndex+1) != FindObjectOfType<TournamentTracker>().currentTournament.opponentOrder.Length) {
				return;
			}
			else {
				FindObjectOfType<CurrentCharacterManager>().currentCharacter.currentTournament++;
				Destroy(tTracker.gameObject);
			}
		}

		levelUpButton.SetActive(lvlUp);
		regularButton.SetActive(!lvlUp);
	}

	public void TriggerLoserWinWidget(int goldLost) {
		GameObject pauseButton = GameObject.Find("Pause Button");
		GameObject speedButton = GameObject.Find("Speed Button");

		if (pauseButton != null) pauseButton.SetActive(false);
		if (speedButton != null) speedButton.SetActive(false);

		loserText.text = string.Format("{0} Wins!", turnManager.winner.actorName);
		loserGoldText.text = string.Format("{0} lost {1} gold...", player.characterData.actorName, goldLost);
		loserWidget.SetActive(true);
	}

	public void UseItem(ItemSO itemData) {
		SetPlayerAction("Items");
		player.combat.SetItem((ConsumableSO)itemData);
	}
}
