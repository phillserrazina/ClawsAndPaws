﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class LevelUpUI : MonoBehaviour {

	// VARIABLES

	[SerializeField] private Text availablePointsText;

	[Space(10)]
	[SerializeField] private Text strengthText;
	[SerializeField] private Text healthText;
	[SerializeField] private Text staminaText;
	[SerializeField] private Text agilityText;
	[SerializeField] private Text intimidationText;

	[Header("Character Components")]
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

	[Space(10)]
	[SerializeField] private Image pointVisual;
	[SerializeField] private CharVisualSO[] allVisuals;
	[SerializeField] private Sprite[] allPointVisuals;

	private int strengthValue = 1;
	private int healthValue = 1;
	private int staminaValue = 1;
	private int agilityValue = 1;
	private int intimidationValue = 1;

	private int initStrengthValue;
	private int initHealthValue;
	private int initStaminaValue;
	private int initAgilityValue;
	private int initIntimidationValue;

	[Space(10)]
	[SerializeField] private GameObject doneButton;

	private CharacterSO player;

	// EXECUTION METHODS

	private void Awake() {
		player = FindObjectOfType<CurrentCharacterManager>().currentCharacter;

		strengthValue = player.strengthPoints;
		healthValue = player.healthPoints;
		staminaValue = player.staminaPoints;
		agilityValue = player.agilityPoints;
		intimidationValue = player.intimidationPoints;

		initStrengthValue = strengthValue;
		initHealthValue = healthValue;
		initStaminaValue = staminaValue;
		initAgilityValue = agilityValue;
		initIntimidationValue = intimidationValue;

		strengthText.text = player.strengthPoints.ToString();
		healthText.text = player.healthPoints.ToString();
		staminaText.text = player.staminaPoints.ToString();
		agilityText.text = player.agilityPoints.ToString();
		intimidationText.text = player.intimidationPoints.ToString();

		UpdateColors(allVisuals[player.visualIndex]);
		pointVisual.sprite = allPointVisuals[player.visualIndex];
	}

	// METHODS

	public void IncreaseAttribute(string a) {
		ChangeAttribute(a, 1);
	}

	public void DecreaseAttribute(string a) {
		ChangeAttribute(a, -1);
	}

	private void ChangeAttribute(string a, int value) {
		switch (a)
		{
			case "Strength":
				if ((strengthValue < 2 && value < 0) || 
					(strengthValue > 9 && value > 0) || 
					(PlayerLevelManager.availableAttributePoints < 1 && value > 0) ||
					(strengthValue == initStrengthValue && value < 0)) return;
				
				PlayerLevelManager.availableAttributePoints -= value;
				strengthValue += value;
				strengthText.text = strengthValue.ToString();
				break;
			
			case "Health":
				if ((healthValue < 2 && value < 0) || 
					(healthValue > 9 && value > 0) || 
					(PlayerLevelManager.availableAttributePoints < 1 && value > 0) ||
					(healthValue == initHealthValue && value < 0)) return;
				
				PlayerLevelManager.availableAttributePoints -= value;
				healthValue += value;
				healthText.text = healthValue.ToString();
				break;
			
			case "Stamina":
				if ((staminaValue < 2 && value < 0) || 
					(staminaValue > 9 && value > 0) || 
					(PlayerLevelManager.availableAttributePoints < 1 && value > 0) ||
					(staminaValue == initStaminaValue && value < 0)) return;
				
				PlayerLevelManager.availableAttributePoints -= value;
				staminaValue += value;
				staminaText.text = staminaValue.ToString();
				break;
			
			case "Agility":
				if ((agilityValue < 2 && value < 0) || 
					(agilityValue > 9 && value > 0) || 
					(PlayerLevelManager.availableAttributePoints < 1 && value > 0) ||
					(agilityValue == initAgilityValue && value < 0)) return;
				
				PlayerLevelManager.availableAttributePoints -= value;
				agilityValue += value;
				agilityText.text = agilityValue.ToString();
				break;
			
			case "Intimidation":
				if ((intimidationValue < 2 && value < 0) || 
					(intimidationValue > 9 && value > 0) || 
					(PlayerLevelManager.availableAttributePoints < 1 && value > 0) ||
					(intimidationValue == initIntimidationValue && value < 0)) return;
				
				PlayerLevelManager.availableAttributePoints -= value;
				intimidationValue += value;
				intimidationText.text = intimidationValue.ToString();
				break;

			default:
				Debug.LogError("CharCreationUI::IncreaseAttribute() --- Invalid Attribute");
				break;
		}

		availablePointsText.text = PlayerLevelManager.availableAttributePoints.ToString();
		var ccManager = FindObjectOfType<CurrentCharacterManager>();
		doneButton.GetComponent<Button>().interactable = (PlayerLevelManager.availableAttributePoints <= 0 || ccManager.currentCharacter.level >= 20);
	}

	public void UpdateCharacter() {
		player.strengthPoints = strengthValue;
		player.healthPoints = healthValue;
		player.staminaPoints = staminaValue;
		player.agilityPoints = agilityValue;
		player.intimidationPoints = intimidationValue;

		PlayerLevelManager.UpdateLevel();

		SaveManager.Save(player);
	}

	private void UpdateColors(CharVisualSO v) {
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
}
