using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCreationUI : MonoBehaviour {

	[SerializeField] private InputField nameInputField;
	[SerializeField] private Text availablePointsText;

	[Space(10)]
	[SerializeField] private Text strengthText;
	[SerializeField] private Text healthText;
	[SerializeField] private Text staminaText;
	[SerializeField] private Text agilityText;
	[SerializeField] private Text intimidationText;

	private int strengthValue = 1;
	private int healthValue = 1;
	private int staminaValue = 1;
	private int agilityValue = 1;
	private int intimidationValue = 1;

	private int availablePoints = 5;

	[Space(10)]
	[SerializeField] private GameObject createButton;

	private void Update() {
		createButton.SetActive(CheckIfAllFieldsAreFilled());
	}

	private bool CheckIfAllFieldsAreFilled() {
		if (nameInputField.text.Length.Equals(0)) return false;

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

		newCharacter.strengthPoints = strengthValue;
		newCharacter.healthPoints = healthValue;
		newCharacter.staminaPoints = staminaValue;
		newCharacter.agilityPoints = agilityValue;
		newCharacter.intimidationPoints = intimidationValue;

		newCharacter.name = "Character_" + newCharacter.actorName;

		FindObjectOfType<CurrentCharacterManager>().SetCharacter(newCharacter);
		SaveManager.CreateNewSaveFile(newCharacter);

		FindObjectOfType<MenuManager>().LoadScene("FightScene");
	}
}
