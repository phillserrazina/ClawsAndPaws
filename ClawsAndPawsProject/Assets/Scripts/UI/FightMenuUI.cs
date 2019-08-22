using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightMenuUI : MonoBehaviour
{
    [SerializeField] private Image attackButton;
    [SerializeField] private Image defendButton;
    [SerializeField] private Image restButton;
    [SerializeField] private Image itemsButton;

    [SerializeField] private Sprite[] attackVisuals;
    [SerializeField] private Sprite[] defendVisuals;
    [SerializeField] private Sprite[] restVisuals;
    [SerializeField] private Sprite[] itemsVisuals;

    private void Awake() {
        CurrentCharacterManager ccManager = FindObjectOfType<CurrentCharacterManager>();
        CharacterSO player = ccManager.currentCharacter;
        int vIndex = player.visualIndex;

        attackButton.sprite = attackVisuals[vIndex];
        defendButton.sprite = defendVisuals[vIndex];
        restButton.sprite = restVisuals[vIndex];
        itemsButton.sprite = itemsVisuals[vIndex];
    }
}
