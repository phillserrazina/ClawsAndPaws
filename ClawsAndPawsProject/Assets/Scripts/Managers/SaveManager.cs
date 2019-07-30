using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager {

	private static string folderPath = Application.persistentDataPath + "/SaveFiles";

	public static void DirectoryCheck() {
		if (!Directory.Exists(folderPath)) {
			Directory.CreateDirectory(folderPath);
		}
	}

	public static void CreateNewSaveFile(CharacterSO data) {
		GameData gameData = new GameData();
		gameData.Create(data);

		string path = folderPath + "/character_" + data.actorName + "_0.json";
		path = CheckForDuplicates(path, 0);
		FileStream stream = File.Create(path);
		stream.Close();
		CustomJson.SaveData(path, gameData);
	}

	public static void CreateDefaultSaveFile() {
		GameData gameData = new GameData();
		gameData.CreateDefault();

		string path = folderPath + "/character_default_0.json";
		path = CheckForDuplicates(path, 0);
		FileStream stream = File.Create(path);
		stream.Close();
		CustomJson.SaveData(path, gameData);
	}

	public static List<string> GetSaveFilesInDirectory() {
		var answer = new List<string>();

		var directoryInfo = new DirectoryInfo(folderPath);
		FileInfo[] fileInfo = directoryInfo.GetFiles();

		foreach (FileInfo info in fileInfo) {
			if (info.Name.Contains("character")) {
				answer.Add(folderPath + "/" + info.Name);
			}
		}

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
