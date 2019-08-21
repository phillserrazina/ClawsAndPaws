using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotUI : MonoBehaviour {

	[SerializeField] private GameObject newCharacterSlotObject;
	[SerializeField] private GameObject existingCharacterSlotObject;

	[SerializeField] private Text characterName;
	[SerializeField] private Text characterLevel;
	[SerializeField] private Image characterVisuals;

	[SerializeField] private Sprite[] allVisuals;

	[SerializeField] private DeleteCharUI deleteConfirmation;

	[HideInInspector] public string assignedPath;
	[HideInInspector] public CharacterSO assignedCharacter;

	public void TriggerNewCharacter() {
		newCharacterSlotObject.SetActive(true);
		existingCharacterSlotObject.SetActive(false);
	}

	public void TriggerExistingCharacter() {
		assignedCharacter.name = assignedCharacter.actorName + " Object";
		characterName.text = assignedCharacter.actorName;
		characterLevel.text = "Level " + assignedCharacter.level;
		characterVisuals.sprite = allVisuals[assignedCharacter.visualIndex];

		newCharacterSlotObject.SetActive(false);
		existingCharacterSlotObject.SetActive(true);

		Button button = existingCharacterSlotObject.GetComponent<Button>();
		button.onClick.AddListener(() => FindObjectOfType<LoadCharacterUI>().LoadCharacter(assignedCharacter, assignedPath));
	}

	public void DeleteCharacter() {
		deleteConfirmation.currentSlot = this;
		deleteConfirmation.gameObject.SetActive(true);
	}
}
