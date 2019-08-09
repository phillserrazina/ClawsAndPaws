using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Held Item", fileName="New Held Item")]
public class HeldItemSO : ItemSO {

	public Effect[] effects;

	public override void Use(Actor target) {
		foreach (Effect effect in effects) {
			ApplyEffect(target, effect);
		}
	}
}
