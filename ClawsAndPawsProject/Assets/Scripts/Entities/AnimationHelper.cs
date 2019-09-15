﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{
    private Actor actor;
    private Combat combat;

    private void Start() {
        actor = GetComponentInParent<Actor>();
        combat = GetComponentInParent<Combat>();
    }

    private void OpponentTakeDamage() {
		ExecuteAttack();
        FindObjectOfType<UIManager>().UpdateUI();

        string animToPlay;
        animToPlay = actor.opponent.stats.currentHealthPoints <= 0 ? "Dead" : "Take Damage";
		actor.opponent.GetComponentInChildren<Animator>().Play(animToPlay);
	}

    private void ExecuteAttack() {
		actor.stats.DepleteStamina(combat.currentAttack.staminaCost);
		float damage = combat.currentAttack.damagePoints + actor.stats.attackPoints;
		actor.opponent.stats.TakeDamage(damage);

		if (combat.currentAttack.conditions != null) {
			foreach (ConditionSO condition in combat.currentAttack.conditions) {
				if (condition.targetSelf) actor.stats.AddCondition(condition);
				else actor.opponent.stats.AddCondition(condition);
			}
		}
	}

    private void Dead() {
        FindObjectOfType<TurnManager>().NextState();
    }
}