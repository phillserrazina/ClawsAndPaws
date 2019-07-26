using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Attack List", fileName="New Attack List")]
public class AttackListSO : ScriptableObject {

	public AttackSO[] attacks;
}
