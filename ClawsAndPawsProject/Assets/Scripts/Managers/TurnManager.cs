using System.Collections;
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
	private Queue<string> choicesQueue = new Queue<string>();

	private Actor[] fighters;
	private Actor winner;

	// EXECUTION METHODS

	private void Start() {
		currentState = States.Start;
		fighters = FindObjectsOfType<Actor>();
	}

	private void Update() {
		StateMachine();
	}

	// METHODS

	private void StateMachine() {
		switch (currentState)
		{
			case States.Start:
				fightQueue = GetFightQueue();
				currentState = States.Choice;
				break;
			
			case States.Choice:
				break;
			
			case States.Execution:
				break;
			
			case States.Aftermath:
				break;
			
			case States.End:
				break;

			default:
				Debug.LogError("TurnManager::State Machine() --- Invalid State");
				break;
		}
	}

	private Queue<Actor> GetFightQueue() {
		Queue<Actor> queue = new Queue<Actor>();

		if (fighters[0].speedPoints < fighters[1].speedPoints) {
			queue.Enqueue(fighters[1]);
			queue.Enqueue(fighters[0]);
		}
		else {
			queue.Enqueue(fighters[0]);
			queue.Enqueue(fighters[1]);
		}

		return queue;
	}

	private void GetChoices() {

	}

	private void ExecuteChoices() {
		while (choicesQueue != null) {
			choicesQueue.Dequeue();
		}
	}

	
}
