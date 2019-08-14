using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TournamentOpponentTracker : MonoBehaviour
{
    public int currentOpponentIndex = 0;

    private void Update() {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "TournamentScene") Destroy(gameObject);
    }
}
