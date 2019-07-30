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

		FoundSaveFiles();
	}

	private void NoSaveFilesFound() {
		foreach (SaveSlotUI saveSlot in saveSlots) {
			saveSlot.TriggerNewCharacter();
		}
	}

	private void FoundSaveFiles() {

	}

	public void CreateNewCharacter() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(CHARACTER_CREATION_SCENE);
	}

	public void LoadCharacter() {
		// TODO: Inject Character data into Game Manager
	}
}
