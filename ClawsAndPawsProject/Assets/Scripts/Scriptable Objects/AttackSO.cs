using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Combat/Attack", fileName="New Attack")]
public class AttackSO : ScriptableObject {

	public Sprite attackIcon;
	public new string name;
	public int requiredLevel;
	public float damagePoints;
	public float staminaCost;
	public float manaCost;
	public ConditionSO[] conditions;
}
