using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TournamentManager : MonoBehaviour
{
    private TournamentTracker tournamentTracker;
    private OpponentSO currentOpponent;

    public void Initialize() {
        tournamentTracker = FindObjectOfType<TournamentTracker>();
        CurrentCharacterManager charManager = FindObjectOfType<CurrentCharacterManager>();

        currentOpponent = tournamentTracker.currentTournament.opponentOrder[GetCurrentOpponentIndex()];

        charManager.SetOpponent(currentOpponent);
        charManager.currentOpponent.Create();
    }

    public void NextFight() {
        TournamentOpponentTracker tracker = FindObjectOfType<TournamentOpponentTracker>();

        if (tracker == null) {
            SceneManager.LoadScene("HubScene");
            return;
        }

        tracker.currentOpponentIndex++;
        DontDestroyOnLoad(tracker.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int GetCurrentOpponentIndex() {
		if (FindObjectOfType<TournamentOpponentTracker>() == null) {
			GameObject go = new GameObject();
            go.name = "Opponent Tracker";
			go.AddComponent<TournamentOpponentTracker>();
			Instantiate(go);
		}

		return FindObjectOfType<TournamentOpponentTracker>().currentOpponentIndex;
	}
}
