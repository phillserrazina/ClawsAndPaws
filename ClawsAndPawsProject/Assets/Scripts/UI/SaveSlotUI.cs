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

	public void TriggerExistingCharacter(string name, string level) {
		characterName.text = name;
		characterLevel.text = "Level " + level;

		newCharacterSlotObject.SetActive(false);
		existingCharacterSlotObject.SetActive(true);
	}
}
