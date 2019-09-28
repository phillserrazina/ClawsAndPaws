using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	// VARIABLES

	private enum States {
		Start,
		Choice,
		Get_Queue,
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

	[SerializeField] private AttackSO[] attackObjects;

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
				if (a.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("DefendIdle")) {
					return false;
				}

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
				UpdateAttackCooldowns();
				if (player.combat.defendCooldown != 0) player.combat.defendCooldown--;
				if (cpu.combat.defendCooldown != 0) cpu.combat.defendCooldown--;

				player.stats.ApplyConditions();
				cpu.stats.ApplyConditions();

				uiManager.UpdateUI();

				currentState = (player.stats.currentHealthPoints <= 0 || cpu.stats.currentHealthPoints <= 0) ? 
								States.Aftermath : 
								States.Choice;
				break;
			
			// ==== PLAYER CHOICE ====
			case States.Choice:
				uiManager.TriggerPlayerChoiceMenu();
				cpu.combat.SetRandomChoice();
				cpu.combat.SetRandomAttack();
				cpu.combat.SetRandomItem();
				break;
			
			// ==== GET FIGHT QUEUE ====
			case States.Get_Queue:
				fightQueue = GetFightQueue();
				currentState = States.Execution;
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
					int goldLost = Inventory.instance.gold / Random.Range(3, 5);
					Inventory.instance.gold -= goldLost;
					uiManager.TriggerLoserWinWidget(goldLost);
				}

				runStateMachine = false;
				Time.timeScale = 1f;
				ResetAttackCooldowns();
				SaveManager.Save(player.characterData);
				break;

			default:
				Debug.LogError("TurnManager::State Machine() --- Invalid State");
				break;
		}
	}

	private Queue<Actor> GetFightQueue() {
		Queue<Actor> queue = new Queue<Actor>();

		// ENEMY GOES FIRST
		if (player.stats.speedPoints <= cpu.stats.speedPoints) {
			// BUT PLAYER DEFENDS OR USES ITEMS
			if ((player.combat.currentChoice == Combat.Actions.Items ||
				player.combat.currentChoice == Combat.Actions.Defend) &&
				(cpu.combat.currentChoice != Combat.Actions.Items &&
				cpu.combat.currentChoice != Combat.Actions.Defend))
			{
				queue.Enqueue(player);
				queue.Enqueue(cpu);
			}
			// AND PLAYER DOES NOT DEFEND OR USE ITEMS
			else {
				queue.Enqueue(cpu);
				queue.Enqueue(player);
			}
		}

		// PLAYER GOES GIRST
		else {
			// BUT ENEMY DEFENDS OR USES ITEMS
			if ((cpu.combat.currentChoice == Combat.Actions.Items ||
				cpu.combat.currentChoice == Combat.Actions.Defend) &&
				(player.combat.currentChoice != Combat.Actions.Items &&
				player.combat.currentChoice != Combat.Actions.Defend))
			{
				queue.Enqueue(cpu);
				queue.Enqueue(player);
			}
			// AND ENEMY DOES NOT USE ITEMS OR DEFENDS
			else {
				queue.Enqueue(player);
				queue.Enqueue(cpu);
			}
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

	private void UpdateAttackCooldowns() {
		foreach (AttackSO a in attackObjects) {
			if (a.currentCooldown > 0) {
				a.currentCooldown--;
			}
		}
	}

	private void ResetAttackCooldowns() {
		foreach (AttackSO a in attackObjects) {
			if (a.currentCooldown > 0) {
				a.currentCooldown = 0;
			}
		}
	}
}
