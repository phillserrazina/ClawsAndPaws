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

			CharacterData retrieveCharacterdData = CustomJson.ReadData(paths[i]).characterData;

			saveSlots[i].TriggerExistingCharacter(retrieveCharacterdData.characterName, "1");
		}
	}

	public void CreateNewCharacter() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(CHARACTER_CREATION_SCENE);
	}

	public void LoadCharacter() {
		// TODO: Inject Data to CurrentCharacterManager
	}
}
