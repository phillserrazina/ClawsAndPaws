using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attack Asset", fileName="New Attack Asset")]
public class AttackSO : ScriptableObject {

	public float damagePoints;
	public ConditionSO[] conditions;
}
