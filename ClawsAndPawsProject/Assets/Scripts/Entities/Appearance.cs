using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Appearance : MonoBehaviour
{
    [SerializeField] private SpriteMeshInstance headRenderer;
    [SerializeField] private SpriteRenderer[] headStripsRenderers;
    [SerializeField] private SpriteRenderer noseRenderer;
    [SerializeField] private SpriteMeshInstance torsoRenderer;
    [SerializeField] private SpriteMeshInstance darkTorsoRenderer;
    [SerializeField] private SpriteMeshInstance tailRenderer;
    [SerializeField] private SpriteMeshInstance rightEarRenderer;
    [SerializeField] private SpriteMeshInstance rightEarShadow;
    [SerializeField] private SpriteMeshInstance leftEarRenderer;
    [SerializeField] private SpriteMeshInstance leftEarShadow;
    [SerializeField] private SpriteMeshInstance frontLeftPawRenderer;
    [SerializeField] private SpriteMeshInstance fronRightPawRenderer;
    [SerializeField] private SpriteMeshInstance backLeftPawRenderer;
    [SerializeField] private SpriteMeshInstance backRightPawRenderer;

    [SerializeField] private CharVisualSO[] allVisuals;

    private int visualIndex;

    public void Initialize() {
        var sRenderer = GetComponentInChildren<SpriteRenderer>();
        var ccManager = FindObjectOfType<CurrentCharacterManager>();
        bool isPlayer = (tag == "Player");
        if (!isPlayer && ccManager.currentOpponent.customCat != null) {
            InstantiateNewCat();
            return;
        }

        visualIndex = isPlayer ? ccManager.currentCharacter.visualIndex : ccManager.currentOpponent.visualIndex;

        var visual = allVisuals[visualIndex];
        AssignVisuals(visual);
    }

    private void AssignVisuals(CharVisualSO v) {
        headRenderer.color = v.normalSkinColor;

        foreach(var s in headStripsRenderers) s.color = v.darkSkinColor;

        noseRenderer.color = v.noseSkinColor;
        torsoRenderer.color = v.normalSkinColor;
        darkTorsoRenderer.color = v.darkSkinColor;
        tailRenderer.color = v.normalSkinColor;

        leftEarRenderer.color = v.normalSkinColor;
        rightEarRenderer.color = v.normalSkinColor;
        leftEarShadow.color = v.darkSkinColor;
        rightEarShadow.color = v.darkSkinColor;
    
        frontLeftPawRenderer.color = v.darkSkinColor;
        fronRightPawRenderer.color = v.normalSkinColor;
        backLeftPawRenderer.color = v.darkSkinColor;
        backRightPawRenderer.color = v.normalSkinColor;
    }

    private void InstantiateNewCat() {
        var ccManager = FindObjectOfType<CurrentCharacterManager>();
        
        GameObject cat = Instantiate(ccManager.currentOpponent.customCat, gameObject.transform.position, gameObject.transform.rotation);
        Actor a = cat.GetComponent<Actor>();
        a.Initialize(false);
        a.opponent.opponent = a;

        a.stats.conditionTab = GetComponent<Stats>().conditionTab;
        
        Destroy(gameObject);
    }
}
