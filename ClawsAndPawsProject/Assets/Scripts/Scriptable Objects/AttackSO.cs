using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Combat/Attack", fileName="New Attack")]
public class AttackSO : ScriptableObject {

	public Sprite attackIcon;
	public new string name;
	[TextArea(1, 3)] public string description;
	public int requiredLevel;
	public float damagePoints;
	public float staminaCost;
	public float manaCost;
	public int cooldown;
	[HideInInspector] public int currentCooldown = 0;
	public ConditionSO[] conditions;
}
