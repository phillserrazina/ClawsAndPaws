using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomJson {
	
	private const string XOR_CODEWORD = "midjSNCjids92nJSN17bsk91n28SIx2z";

	public static void SaveData(string path, GameData data, bool xor=false) {
		// Get file path
		string filePath = path;

		if (System.IO.File.Exists(filePath) == false) {
			Debug.LogError("Unable to read file at " + filePath + "; File does not exist");
			return;
		}

		// Initialize wrapper
		JsonWrapper wrapper = new JsonWrapper();
		wrapper.gameData = data;
		// Form contents, encrypt them and write them to the file
		string contents = JsonUtility.ToJson(wrapper, true);
		if (xor) contents = XorEncryption(contents);
		System.IO.File.WriteAllText (filePath, contents);
	}


	public static GameData ReadData(string path, bool xor=false) {
		// Get file path
		string filePath = path;

		if (System.IO.File.Exists(filePath) == false) {
			Debug.LogError("Unable to read file at " + filePath + "; File does not exist");
			return null;
		}

		// Get file and decrypt them contents
		string contents = System.IO.File.ReadAllText(filePath);
		if (xor) contents = XorEncryption(contents);

		// Get game data from retrieved file
		JsonWrapper wrapper = JsonUtility.FromJson<JsonWrapper>(contents);

		if (wrapper == null) {
			Debug.LogError("File at " + filePath + " is corrupted! No wrapper was found");
			return null;
		}

		return wrapper.gameData;
	}

	
	private static string XorEncryption(string message) {
		string ans = "";

		for (int i = 0; i < message.Length; i++) {
			ans += (char)(message[i] ^ XOR_CODEWORD[i % XOR_CODEWORD.Length]);
		}

		return ans;
	}
	
}
