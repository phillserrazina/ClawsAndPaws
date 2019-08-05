using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Have this be read from a save file instead of an SO
[CreateAssetMenu(menuName="Actors/Character Data", fileName="New Character Data")]
public class CharacterSO : ScriptableObject {

	public string actorName;
	public int experiencePoints;
	
	public int strengthPoints;
	public int agilityPoints;
	public int healthPoints;
	public int staminaPoints;
	public int intimidationPoints;
}
