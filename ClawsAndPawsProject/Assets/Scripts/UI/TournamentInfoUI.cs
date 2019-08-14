using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TournamentInfoUI : MonoBehaviour
{
    private TournamentTracker tournamentTracker;

    [SerializeField] private Text tournamentTitle;
    [SerializeField] private Text contenstantsText;

    [SerializeField] private GameObject noTournamentAvailableObject;
    [SerializeField] private GameObject tournamentAvailableObject;

    [SerializeField] private GameObject notEnoughLevelText;
    [SerializeField] private GameObject fightButton;

    public void Start() {
        tournamentTracker = FindObjectOfType<TournamentTracker>();
        CurrentCharacterManager charManager = FindObjectOfType<CurrentCharacterManager>();
        int currentTournamentIndex = charManager.currentCharacter.currentTournament;

        bool availableTournament = (currentTournamentIndex < tournamentTracker.allTournaments.Length);
        noTournamentAvailableObject.SetActive(!availableTournament);
        tournamentAvailableObject.SetActive(availableTournament);

        if (!availableTournament) return;

        tournamentTitle.text = tournamentTracker.currentTournament.name;
        contenstantsText.text = GetContestantsText();

        bool playerHasLevel = (FindObjectOfType<CurrentCharacterManager>().currentCharacter.level >= tournamentTracker.currentTournament.requiredLevel);
        notEnoughLevelText.GetComponent<Text>().text = string.Format("You need to be level {0} to enter!", tournamentTracker.currentTournament.requiredLevel);
        notEnoughLevelText.SetActive(!playerHasLevel);
        fightButton.SetActive(playerHasLevel);
    }

    private string GetContestantsText() {
        string answer = "";

        foreach (OpponentSO t in tournamentTracker.currentTournament.opponentOrder) {
            answer += string.Format("- {0} \n", t.actorName);
        }

        return answer;
    }
}
