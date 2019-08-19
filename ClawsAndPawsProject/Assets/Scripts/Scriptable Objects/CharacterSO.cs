using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Actors/Character Data", fileName="New Character Data")]
public class CharacterSO : ScriptableObject {

	public string actorName;
	public int experiencePoints;
	public int level;
	
	public int strengthPoints;
	public int agilityPoints;
	public int healthPoints;
	public int staminaPoints;
	public int intimidationPoints;

	public int currentTournament;
	public int visualIndex;
}
