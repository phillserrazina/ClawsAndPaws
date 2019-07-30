using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCharacterManager : MonoBehaviour {

	private CharacterSO currentCharacter;

	public void SetCharacter(CharacterSO character) {
		currentCharacter = character;
	}
}
