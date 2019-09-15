﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	// VARIABLES

	private enum States {
		Start,
		Choice,
		Execution,
		Aftermath,
		End
	}

	private States currentState;

	private Queue<Actor> fightQueue = new Queue<Actor>();

	private Actor player;
	private Actor cpu;

	public Actor winner { get; private set; }

	private bool runStateMachine = false;

	[SerializeField] private RewardsUI rewardsUI;
	private UIManager uiManager;

	// EXECUTION METHODS

	private void Update() {
		StateMachine();
	}

	// METHODS

	public void Initialize() {
		currentState = States.Start;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
		cpu = player.opponent;
		uiManager = FindObjectOfType<UIManager>();

		runStateMachine = true;
	}

	private bool CheckIfAnimationIsPlaying() {
		foreach (Actor a in FindObjectsOfType<Actor>()) {
			if (!a.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
				return true;
			}
		}

		return false;
	}

	private void StateMachine() {

		if (runStateMachine == false) return;

		switch (currentState)
		{
			// ==== START ====
			case States.Start:
				currentState = States.Choice;
				player.stats.ApplyConditions();
				cpu.stats.ApplyConditions();
				uiManager.UpdateUI();
				break;
			
			// ==== PLAYER CHOICE ====
			case States.Choice:
				uiManager.TriggerPlayerChoiceMenu();
				cpu.combat.SetRandomChoice();
				cpu.combat.SetRandomAttack();
				fightQueue = GetFightQueue();
				break;
			
			// ==== EXECUTION ====
			case States.Execution:
				ExecuteFightQueue();
				break;
			
			// ==== AFTERMATH ====
			case States.Aftermath:
				CalculateAftermath();
				uiManager.UpdateUI();
				break;
			
			// ==== FIGHT END ====
			case States.End:

				if (winner == player) {
					OpponentSO opponentData = cpu.characterData as OpponentSO;

					rewardsUI.battleEndXP = player.characterData.experiencePoints;
					rewardsUI.battleEndGold = Inventory.instance.gold;
					rewardsUI.itemRewardObject.PushToRewardStack(opponentData.itemRewards.ToArray());

					player.characterData.experiencePoints += opponentData.xpReward;
					Inventory.instance.gold += opponentData.goldReward;
					Inventory.instance.Add(opponentData.itemRewards.ToArray());

					bool lvlUp = PlayerLevelManager.CheckLevel();

					uiManager.TriggerPlayerWinWidget(opponentData.goldReward, opponentData.xpReward, lvlUp);
				}
				else {
					Inventory.instance.gold /= 2;
					uiManager.TriggerLoserWinWidget(Inventory.instance.gold);
				}

				runStateMachine = false;
				SaveManager.Save(player.characterData);
				break;

			default:
				Debug.LogError("TurnManager::State Machine() --- Invalid State");
				break;
		}
	}

	private Queue<Actor> GetFightQueue() {
		Queue<Actor> queue = new Queue<Actor>();

		if (player.stats.speedPoints <= cpu.stats.speedPoints) {
			queue.Enqueue(cpu);
			queue.Enqueue(player);
		}
		else {
			queue.Enqueue(player);
			queue.Enqueue(cpu);
		}

		return queue;
	}

	private void ExecuteFightQueue() {
		if (CheckIfAnimationIsPlaying()) return;

		if (CheckIfSomeoneIsDead()) {
			currentState = States.Aftermath;
			return;
		}

		fightQueue.Dequeue().combat.ExecuteAction();
		uiManager.UpdateUI();
	}

	private bool CheckIfSomeoneIsDead() {
		return (fightQueue.Count == 0 || 
				player.stats.currentHealthPoints <= 0 || 
				cpu.stats.currentHealthPoints <= 0);
	}

	private void CalculateAftermath() {
		if (player.stats.currentHealthPoints <= 0) {
			winner = cpu;
			currentState = States.End;
			return;
		}

		if (cpu.stats.currentHealthPoints <= 0) {
			winner = player;
			currentState = States.End;
			return;
		}

		currentState = States.Start;
	}

	public void NextState() {
		currentState = (States)(((int)currentState + 1) % 5);
	}

	public void Surrender() {
		winner = cpu;
		currentState = States.End;
		GameObject pMenu = GameObject.Find("Pause Menu");
		GameObject pButton = GameObject.Find("Pause Button");
		if (pMenu != null) pMenu.SetActive(false);
		if (pButton != null) pButton.SetActive(false);
	}
}
