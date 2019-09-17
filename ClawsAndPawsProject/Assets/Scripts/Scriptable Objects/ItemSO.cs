using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Key Item", fileName="New Key Item")]
public class ItemSO : ScriptableObject {

	[System.Serializable]
	public struct Effect {
		public enum Effects {
			Change_Health,
			Change_Stamina,
			Change_Speed,
			Change_Defense,
			Change_Attack,
			Change_Intimidation
		}

		public Effects effect;
		public float strength;
	}

	public new string name;
	public Sprite icon;
	[TextArea(1, 3)]
	public string description;

	public virtual void Use(Actor target) { }

	protected void ApplyEffect(Actor target, Effect effect) {
		switch (effect.effect)
		{
			case Effect.Effects.Change_Health:
				if (effect.strength > 0) target.stats.RestoreHealth(effect.strength);
				else target.stats.TakeDamage(-effect.strength, true);
				break;

			case Effect.Effects.Change_Stamina:
				if (effect.strength > 0) target.stats.RestoreStamina(effect.strength);
				else target.stats.DepleteStamina(-effect.strength);
				break;
			
			case Effect.Effects.Change_Defense:
				target.stats.defensePoints += effect.strength;
				if (target.stats.defensePoints < 0) target.stats.defensePoints = 0;
				break;
			
			case Effect.Effects.Change_Attack:
				target.stats.attackPoints += effect.strength;
				if (target.stats.attackPoints < 0) target.stats.attackPoints = 0;
				break;
			
			case Effect.Effects.Change_Speed:
				target.stats.speedPoints += effect.strength;
				if (target.stats.speedPoints < 0) target.stats.speedPoints = 0;
				break;
			
			case Effect.Effects.Change_Intimidation:
				target.stats.intimidationPoints += effect.strength;
				if (target.stats.intimidationPoints < 0) target.stats.intimidationPoints = 0;
				break;

			default:
				Debug.LogError("ItemSO::ApplyEffect() --- Invalid Effect");
				break;
		}
	}
}
