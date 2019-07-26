using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Character Data", fileName="New Character Data")]
public class CharacterSO : ScriptableObject {

	public string actorName;
	public float healthPoints;
	public float staminaPoints;
	public float speedPoints;
	public float attackPoints;
}
