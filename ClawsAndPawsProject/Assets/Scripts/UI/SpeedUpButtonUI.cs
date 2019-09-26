using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedUpButtonUI : MonoBehaviour
{
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;

    private Image image;
    private GameManager gameManager;

    private void Start() {
        image = GetComponent<Image>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if (image == null) return;

        image.sprite = gameManager.isSpedUp ? onSprite : offSprite;
    }
}
