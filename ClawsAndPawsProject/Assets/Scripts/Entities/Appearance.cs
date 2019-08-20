using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private Sprite[] allVisuals;

    private int visualIndex;

    public void Initialize() {
        var sRenderer = GetComponentInChildren<SpriteRenderer>();
        var ccManager = FindObjectOfType<CurrentCharacterManager>();
        bool isPlayer = (tag == "Player");
        if (!isPlayer && ccManager.currentOpponent.customSprite != null) {
            sRenderer.sprite = ccManager.currentOpponent.customSprite;
            return;
        }

        visualIndex = isPlayer ? ccManager.currentCharacter.visualIndex : ccManager.currentOpponent.visualIndex;

        sRenderer.sprite = allVisuals[visualIndex];
    }
}
