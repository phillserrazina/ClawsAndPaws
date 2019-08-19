using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private Sprite[] allVisuals;

    private int visualIndex;

    public void Initialize() {
        var ccManager = FindObjectOfType<CurrentCharacterManager>();
        visualIndex = (tag == "Player") ? ccManager.currentCharacter.visualIndex : ccManager.currentOpponent.visualIndex;

        GetComponentInChildren<SpriteRenderer>().sprite = allVisuals[visualIndex];
    }
}
