using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharCreationUI : MonoBehaviour {

	[SerializeField] private InputField nameInputField;

	[Space(10)]
	[SerializeField] private InputField strengthInputField;
	[SerializeField] private InputField healthInputField;
	[SerializeField] private InputField staminaInputField;
	[SerializeField] private InputField agilityInputField;
	[SerializeField] private InputField intimidationInputField;

	[Space(10)]
	[SerializeField] private GameObject createButton;

	private void Update() {
		createButton.SetActive(CheckIfAllFieldsAreFilled());
	}

	private bool CheckIfAllFieldsAreFilled() {
		if (nameInputField.text.Length.Equals(0)) return false;

		return true;
	}

	public void CreateNewCharacter() {
		var newCharacter = ScriptableObject.CreateInstance<CharacterSO>();

		newCharacter.actorName = nameInputField.text;
		newCharacter.strengthPoints = float.Parse(strengthInputField.text);
		newCharacter.healthPoints = float.Parse(healthInputField.text);
		newCharacter.staminaPoints = float.Parse(staminaInputField.text);
		newCharacter.agilityPoints = float.Parse(agilityInputField.text);
		newCharacter.intimidationPoints = float.Parse(intimidationInputField.text);

		newCharacter.name = "Character_" + newCharacter.actorName;

		FindObjectOfType<CurrentCharacterManager>().SetCharacter(newCharacter);
		SaveManager.CreateNewSaveFile(newCharacter);
	}
}
