using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData {

	public CharacterData characterData = new CharacterData();

    public void Create(CharacterSO data) {
        characterData.Create(data);
    }

    public void CreateDefault() {
        characterData.CreateDefault();
    }
}
