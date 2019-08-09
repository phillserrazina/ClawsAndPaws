using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

	public CharacterData characterData = new CharacterData();
    public List<string> inventoryData = new List<string>();

    public void Create(CharacterSO data) {
        characterData.Create(data);
    }

    public void CreateDefault() {
        characterData.CreateDefault();
    }
}
