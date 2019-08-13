using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentTracker : MonoBehaviour
{
    public static TournamentTracker instance;

    public TournamentSO[] allTournaments;
    public TournamentSO currentTournament { get; private set; }

    private void Awake() {
        Singleton();

        int currentTournamentIndex = FindObjectOfType<CurrentCharacterManager>().currentCharacter.currentTournament;
        bool availableTournament = (currentTournamentIndex < allTournaments.Length);

        if (availableTournament)
            currentTournament = allTournaments[currentTournamentIndex];
        else
            currentTournament = null;
    }

    private void Singleton() {
        if (instance == null)
			instance = this;
		else if (instance != this) {
			Destroy(instance.gameObject);
			instance = this;
		}

		DontDestroyOnLoad(gameObject);
    }
}
