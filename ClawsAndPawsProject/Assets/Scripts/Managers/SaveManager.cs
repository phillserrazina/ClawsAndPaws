using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour {

	private static string folderPath = Application.persistentDataPath + "/SaveFiles";

	public static void CreateNewSaveFile(CharacterSO data) {
		GameData gameData = new GameData();
		gameData.CreateDefault();

		string path = folderPath + "character_" + data.actorName + "_0.json";
		path = CheckForDuplicates(path, 0);
		CustomJson.SaveData(path, gameData);
	}

	public static void CreateDefaultSaveFile() {
		GameData gameData = new GameData();
		gameData.CreateDefault();

		string path = folderPath + "character_default_0.json";
		path = CheckForDuplicates(path, 0);
		CustomJson.SaveData(path, gameData);
	}

	private List<string> GetSaveFilesInDirectory() {
		var answer = new List<string>();

		var directoryInfo = new DirectoryInfo(folderPath);
		FileInfo[] fileInfo = directoryInfo.GetFiles();

		foreach (FileInfo info in fileInfo) {
			if (info.Name.Contains("character")) {
				answer.Add(info.Name);
			}
		}

		return answer;
	}

	private static string CheckForDuplicates(string fileName, int index) {

		var directoryInfo = new DirectoryInfo(folderPath);
		FileInfo[] fileInfo = directoryInfo.GetFiles();

		foreach (FileInfo info in fileInfo) {
			if (info.Name.Equals(fileName)) {
				index++;
				
				fileName = fileName.TrimEnd();
				fileName = fileName + index;

				fileName = CheckForDuplicates(fileName, index);
			}
		}

		return fileName;
	}
}
