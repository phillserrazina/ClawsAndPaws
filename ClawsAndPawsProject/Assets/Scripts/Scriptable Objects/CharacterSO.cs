using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Have this be read from a save file instead of an SO
[CreateAssetMenu(menuName="Character Data", fileName="New Character Data")]
public class CharacterSO : ScriptableObject {

	public string actorName;
	
	public float strengthPoints;
	public float agilityPoints;
	public float healthPoints;
	public float staminaPoints;
	public float intimidationPoints;
}
