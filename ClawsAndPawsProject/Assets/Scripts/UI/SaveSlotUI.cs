using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour {

	[SerializeField] private GameObject newCharacterSlotObject;
	[SerializeField] private GameObject existingCharacterSlotObject;

	[SerializeField] private Text characterName;
	[SerializeField] private Text characterLevel;
	[SerializeField] private Image characterVisuals; // TODO

	public void TriggerNewCharacter() {
		newCharacterSlotObject.SetActive(true);
		existingCharacterSlotObject.SetActive(false);
	}

	public void TriggerExistingCharacter(CharacterSO character) {
		character.name = character.actorName + " Object";
		characterName.text = character.actorName;
		int level = Mathf.FloorToInt((character.experiencePoints + 1000) / 1000);
		characterLevel.text = "Level " + level;

		newCharacterSlotObject.SetActive(false);
		existingCharacterSlotObject.SetActive(true);

		Button button = existingCharacterSlotObject.GetComponent<Button>();
		button.onClick.AddListener(() => FindObjectOfType<LoadCharacterUI>().LoadCharacter(character));
	}
}
