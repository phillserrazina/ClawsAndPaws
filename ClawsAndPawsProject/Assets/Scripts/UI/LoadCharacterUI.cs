using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacterUI : MonoBehaviour {

	public SaveSlotUI[] saveSlots;

	private const string CHARACTER_CREATION_SCENE = "CharCreation";

	private void Start() {
		SaveManager.DirectoryCheck();
		var existingSaveFiles = SaveManager.GetSaveFilesInDirectory();

		if (existingSaveFiles.Count <= 0) {
			NoSaveFilesFound();
			return;
		}

		FoundSaveFiles(existingSaveFiles);
	}

	private void NoSaveFilesFound() {
		foreach (SaveSlotUI saveSlot in saveSlots) {
			saveSlot.TriggerNewCharacter();
		}
	}

	private void FoundSaveFiles(List<string> paths) {
		for (int i = 0; i < paths.Count; i++) {
			if (paths[i] == null) {
				saveSlots[i].TriggerNewCharacter();
				continue;
			}

			CharacterData retrieveCharacterdData = SaveManager.Load(paths[i]).characterData;

			saveSlots[i].assignedPath = paths[i];
			saveSlots[i].assignedCharacter = retrieveCharacterdData.GetSO();
			saveSlots[i].TriggerExistingCharacter();
		}
	}

	public void CreateNewCharacter() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(CHARACTER_CREATION_SCENE);
	}

	public void LoadCharacter(CharacterSO character, string savePath) {
		SaveManager.currentSavePath = savePath;
		FindObjectOfType<CurrentCharacterManager>().SetCharacter(character);
		UnityEngine.SceneManagement.SceneManager.LoadScene("HubScene");
	}
}
