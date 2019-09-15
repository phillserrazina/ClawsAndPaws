using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Anima2D;

public class CharCreationUI : MonoBehaviour {

	[SerializeField] private InputField nameInputField;
	[SerializeField] private Text availablePointsText;
	[SerializeField] private Image characterVisual;
	[SerializeField] private Image characterPointsVisual;

	[Space(10)]
	[SerializeField] private Text strengthText;
	[SerializeField] private Text healthText;
	[SerializeField] private Text staminaText;
	[SerializeField] private Text agilityText;
	[SerializeField] private Text intimidationText;

	[SerializeField] private CharVisualSO[] allVisuals;
	[SerializeField] private Sprite[] allPointsVisuals;

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

	private int strengthValue = 1;
	private int healthValue = 1;
	private int staminaValue = 1;
	private int agilityValue = 1;
	private int intimidationValue = 1;

	private int visualIndexValue = 0;

	private int availablePoints = 5;

	[Space(10)]
	[SerializeField] private GameObject createButton;

	private void Update() {
		createButton.GetComponent<Button>().interactable = CheckIfAllFieldsAreFilled();
	}

	private bool CheckIfAllFieldsAreFilled() {
		if (nameInputField.text.Length.Equals(0) || availablePoints > 0) return false;

		return true;
	}

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
				if ((strengthValue < 2 && value < 0) || (strengthValue > 9 && value > 0) || (availablePoints < 1 && value > 0)) return;
				availablePoints -= value;
				strengthValue += value;
				strengthText.text = strengthValue.ToString();
				break;
			
			case "Health":
				if ((healthValue < 2 && value < 0) || (healthValue > 9 && value > 0) || (availablePoints < 1 && value > 0)) return;
				availablePoints -= value;
				healthValue += value;
				healthText.text = healthValue.ToString();
				break;
			
			case "Stamina":
				if ((staminaValue < 2 && value < 0) || (staminaValue > 9 && value > 0) || (availablePoints < 1 && value > 0)) return;
				availablePoints -= value;
				staminaValue += value;
				staminaText.text = staminaValue.ToString();
				break;
			
			case "Agility":
				if ((agilityValue < 2 && value < 0) || (agilityValue > 9 && value > 0) || (availablePoints < 1 && value > 0)) return;
				availablePoints -= value;
				agilityValue += value;
				agilityText.text = agilityValue.ToString();
				break;
			
			case "Intimidation":
				if ((intimidationValue < 2 && value < 0) || (intimidationValue > 9 && value > 0) || (availablePoints < 1 && value > 0)) return;
				availablePoints -= value;
				intimidationValue += value;
				intimidationText.text = intimidationValue.ToString();
				break;
			
			case "Visual":
				visualIndexValue += value;

				if (visualIndexValue >= allVisuals.Length) visualIndexValue = 0;
				if (visualIndexValue < 0) visualIndexValue = allVisuals.Length-1;

				UpdateColors(allVisuals[visualIndexValue]);
				characterPointsVisual.sprite = allPointsVisuals[visualIndexValue];
				break;

			default:
				Debug.LogError("CharCreationUI::IncreaseAttribute() --- Invalid Attribute");
				break;
		}

		availablePointsText.text = availablePoints.ToString();
	}

	public void CreateNewCharacter() {
		var newCharacter = ScriptableObject.CreateInstance<CharacterSO>();

		newCharacter.actorName = nameInputField.text;
		newCharacter.experiencePoints = 0;
		newCharacter.level = 1;

		newCharacter.strengthPoints = strengthValue;
		newCharacter.healthPoints = healthValue;
		newCharacter.staminaPoints = staminaValue;
		newCharacter.agilityPoints = agilityValue;
		newCharacter.intimidationPoints = intimidationValue;

		newCharacter.currentTournament = 0;
		newCharacter.visualIndex = visualIndexValue;

		newCharacter.name = "Character_" + newCharacter.actorName;

		FindObjectOfType<CurrentCharacterManager>().SetCharacter(newCharacter);
		SaveManager.currentSavePath = SaveManager.CreateNewSaveFile(newCharacter);
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
