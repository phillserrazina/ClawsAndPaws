using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour
{
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer noseRenderer;
    [SerializeField] private SpriteRenderer mouthRenderer;
    [SerializeField] private SpriteRenderer torsoRenderer;
    [SerializeField] private SpriteRenderer tailRenderer;
    [SerializeField] private SpriteRenderer eyeRenderer;
    [SerializeField] private SpriteRenderer whiskersRenderer;
    [SerializeField] private SpriteRenderer rightEarRenderer;
    [SerializeField] private SpriteRenderer leftEarRenderer;
    [SerializeField] private SpriteRenderer frontLeftPawRenderer;
    [SerializeField] private SpriteRenderer fronRightPawRenderer;
    [SerializeField] private SpriteRenderer backLeftPawRenderer;
    [SerializeField] private SpriteRenderer backRightPawRenderer;

    [SerializeField] private CharVisualSO[] allNewVisuals;
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

        var visual = allNewVisuals[visualIndex];
        AssignVisuals(visual);
    }

    private void AssignVisuals(CharVisualSO v) {
        headRenderer.sprite = v.head;
        noseRenderer.sprite = v.nose;
        mouthRenderer.sprite = v.mouth;
        torsoRenderer.sprite = v.torso;
        tailRenderer.sprite = v.tail;
        eyeRenderer.sprite = v.eye;

        whiskersRenderer.sprite = v.whiskers;
        rightEarRenderer.sprite = v.rightEar;
    
        frontLeftPawRenderer.sprite = v.frontLeftPaw;
        fronRightPawRenderer.sprite = v.frontRightPaw;
        backLeftPawRenderer.sprite = v.backLeftPaw;
        backRightPawRenderer.sprite = v.backRightPaw;
    }
}
