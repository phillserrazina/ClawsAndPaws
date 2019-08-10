using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TournamentManager : MonoBehaviour
{
    public TournamentSO[] allTournaments;
    public TournamentSO currentTournament { get; private set; }
    private OpponentSO currentOpponent;

    public void Initialize() {
        CurrentCharacterManager charManager = FindObjectOfType<CurrentCharacterManager>();

        int currentTournamentIndex = charManager.currentCharacter.currentTournament;
        currentTournament = allTournaments[currentTournamentIndex];

        currentOpponent = currentTournament.opponentOrder[GetCurrentOpponentIndex()];

        charManager.SetOpponent(currentOpponent);
        charManager.currentOpponent.Create();
    }

    public void NextFight() {
        TournamentTracker tracker = FindObjectOfType<TournamentTracker>();

        if ((tracker.currentOpponentIndex+1) == currentTournament.opponentOrder.Length) {
            SceneManager.LoadScene("HubScene");
            return;
        }

        tracker.currentOpponentIndex++;
        DontDestroyOnLoad(tracker.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private int GetCurrentOpponentIndex() {
		if (FindObjectOfType<TournamentTracker>() == null) {
			GameObject go = new GameObject();
            go.name = "Opponent Tracker";
			go.AddComponent<TournamentTracker>();
			Instantiate(go);
		}

		return FindObjectOfType<TournamentTracker>().currentOpponentIndex;
	}
}
