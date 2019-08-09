using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager {

	private static string folderPath = Application.persistentDataPath + "/SaveFiles";
	public static string currentSavePath;

	public static void DirectoryCheck() {
		if (!Directory.Exists(folderPath)) {
			Directory.CreateDirectory(folderPath);
		}
	}

	public static void Save(CharacterSO data) {
		GameData gameData = CustomJson.ReadData(currentSavePath);

		gameData.characterData.Create(data);
		gameData.inventoryData = Inventory.instance.GetInventoryData();
		CustomJson.SaveData(currentSavePath, gameData);
	} 

	public static GameData Load(string path) {
		return CustomJson.ReadData(path);
	}

	public static GameData LoadCurrentSaveData() {
		return CustomJson.ReadData(currentSavePath);
	}

	public static string CreateNewSaveFile(CharacterSO data) {
		GameData gameData = new GameData();
		gameData.Create(data);

		string path = folderPath + "/Character_" + data.actorName + "_0.json";
		path = CheckForDuplicates(path, 0);
		FileStream stream = File.Create(path);
		stream.Close();
		CustomJson.SaveData(path, gameData);

		return path;
	}

	public static string CreateDefaultSaveFile() {
		GameData gameData = new GameData();
		gameData.CreateDefault();

		string path = folderPath + "/Character_Default_0.json";
		path = CheckForDuplicates(path, 0);
		FileStream stream = File.Create(path);
		stream.Close();
		CustomJson.SaveData(path, gameData);

		return path;
	}

	public static List<string> GetSaveFilesInDirectory() {
		var answer = new List<string>();

		var directoryInfo = new DirectoryInfo(folderPath);
		FileInfo[] fileInfo = directoryInfo.GetFiles();

		foreach (FileInfo info in fileInfo) {
			if (info.Name.Contains("Character")) {
				answer.Add(folderPath + "/" + info.Name);
			}
		}

		Debug.Log("Number of Files Found: " + answer.Count);
		return answer;
	}

	private static string CheckForDuplicates(string fileName, int index) {

		if (File.Exists(fileName)) {
			index++;
			
			fileName = fileName.Remove(fileName.Length - 6);
			fileName = fileName + index + ".json";

			fileName = CheckForDuplicates(fileName, index);
		}

		return fileName;
	}
}
