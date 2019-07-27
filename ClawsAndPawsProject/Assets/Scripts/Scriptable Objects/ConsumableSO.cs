using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Consumable Item", fileName="New Consumable Item")]
public class ConsumableSO : ItemSO {

	[System.Serializable]
	public new struct Effect {

		public ItemSO.Effect effect;

		[Space(5)]
		public float duration;
		public bool infinite;
	}

	public Effect[] effects;
}
