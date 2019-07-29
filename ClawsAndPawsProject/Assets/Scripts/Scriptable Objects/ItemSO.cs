using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Key Item", fileName="New Key Item")]
public class ItemSO : ScriptableObject {

	[System.Serializable]
	public struct Effect {
		public enum Effects {
			Change_Health,
			Change_Speed,
			Change_Defense,
			Change_Attack
		}

		public Effects effect;
		public float strength;
	}

	public new string name;
	public Sprite icon;
	[TextArea(1, 3)]
	public string description;

	public virtual void Use(Actor target) {

	}

	protected void ApplyEffect(Actor target, Effect effect) {
		switch (effect.effect)
		{
			case Effect.Effects.Change_Health:
				if (effect.strength > 0) target.stats.RestoreHealth(effect.strength);
				else target.stats.TakeDamage(effect.strength, true);
				break;
			
			case Effect.Effects.Change_Defense:
				break;
			
			case Effect.Effects.Change_Attack:
				if (effect.strength > 0) target.stats.attackPoints += effect.strength;
				else target.stats.attackPoints -= effect.strength;
				break;
			
			case Effect.Effects.Change_Speed:
				if (effect.strength > 0) target.stats.speedPoints += effect.strength;
				else target.stats.speedPoints -= effect.strength;
				break;

			default:
				Debug.LogError("ItemSO::ApplyEffect() --- Invalid Effect");
				break;
		}
	}
}
