using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Key Item", fileName="New Key Item")]
public class ItemSO : ScriptableObject {

	[System.Serializable]
	public struct Effect {
		public enum Effects {
			Reduce_Health,
			Increase_Health,

			Reduce_Speed,
			Increase_Speed,

			Reduce_Defense,
			Increase_Defense,

			Reduce_Attack,
			Increase_Attack
		}

		public Effects effect;
		public float strength;
	}

	public new string name;
	public Sprite icon;
	[TextArea(1, 3)]
	public string description;
}
