using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionSO {

	public enum Conditions {
		Poison,
		Slow,
		Reduce_Defense,
		Increase_Defense,
		Sleep
	}

	public enum SelfConditions {

	}

	public Conditions conditions;
	public SelfConditions selfConditions;
	public float duration;
	public float strength;
}
