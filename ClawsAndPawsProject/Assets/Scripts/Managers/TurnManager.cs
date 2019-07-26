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

	// EXECUTION METHODS

	private void Start() {
		currentState = States.Start;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
		cpu = player.opponent;
	}

	private void Update() {
		StateMachine();
	}

	// METHODS

	private void StateMachine() {
		switch (currentState)
		{
			// ==== START ====
			case States.Start:
				currentState = States.Choice;
				break;
			
			// ==== PLAYER CHOICE ====
			case States.Choice:
				FindObjectOfType<UIManager>().TriggerPlayerChoiceMenu();
				cpu.combat.SetRandomChoice();
				fightQueue = GetFightQueue();
				break;
			
			// ==== EXECUTION ====
			case States.Execution:
				ExecuteFightQueue();
				break;
			
			// ==== AFTERMATH ====
			case States.Aftermath:
				CalculateAftermath();
				FindObjectOfType<UIManager>().UpdateUI();
				break;
			
			// ==== FIGHT END ====
			case States.End:
				FindObjectOfType<UIManager>().TriggerWinnerWidget();
				break;

			default:
				Debug.LogError("TurnManager::State Machine() --- Invalid State");
				break;
		}
	}

	private Queue<Actor> GetFightQueue() {
		Queue<Actor> queue = new Queue<Actor>();

		if (player.stats.speedPoints < cpu.stats.speedPoints) {
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
		if (fightQueue.Count == 0) {
			currentState = States.Aftermath;
			return;
		}

		fightQueue.Dequeue().combat.ExecuteAction();
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
}