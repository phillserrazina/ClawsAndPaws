using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Condition", fileName="New Condition")]
public class ConditionSO : ScriptableObject {

	public enum Conditions {
		Poison,
		Sleep,

		Reduce_Speed,
		Increase_Speed,

		Reduce_Defense,
		Increase_Defense,

		Reduce_Attack,
		Increase_Attack
	}

	public Conditions condition;
	public bool targetSelf;
	public float duration;
	public float strength;
}
