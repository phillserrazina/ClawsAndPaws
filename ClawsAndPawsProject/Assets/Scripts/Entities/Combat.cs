﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Actor))]
public class Combat : MonoBehaviour {

	// VARIABLES

	[SerializeField] private SpriteRenderer itemAnimationRenderer;

	public enum Actions {
		Attack,
		Items,
		Defend,
		Rest
	}

	public Actions currentChoice { get; private set; }
	public void SetChoice(string choice) { currentChoice = (Actions)System.Enum.Parse(typeof(Actions), choice); }
	public void SetChoice(Actions choice) { currentChoice = choice; }
	public void SetRandomChoice() { SetChoice(GetActionChoice()); }

	public AttackSO currentAttack { get; private set; }
	public void SetAttack(AttackSO attackData) { currentAttack = attackData; }
	public void SetRandomAttack() { 
		AttackSO chosen = attackList.attacks[Random.Range(0, attackList.attacks.Length)];
		if ((actor.characterData.level >= chosen.requiredLevel) && chosen.currentCooldown <= 0)
			SetAttack(chosen);
		else
			SetRandomAttack();
	}

	public ConsumableSO currentItem { get; private set; }
	public void SetItem(ConsumableSO itemData) { currentItem = itemData; }
	public void SetRandomItem() { 
		ItemListSO allItems = Resources.Load("All Items") as ItemListSO;

		float val = Random.Range(0, 500);
		if (val < 100)
			SetItem((ConsumableSO)allItems.Search("Health Potion"));
		else if (val < 250)
			SetItem((ConsumableSO)allItems.Search("Attack Potion"));
		else if (val < 350)
			SetItem((ConsumableSO)allItems.Search("Speed Potion"));
		else
			SetItem((ConsumableSO)allItems.Search("Defense Potion"));
	}

	public AttackListSO attackList;
	
	public bool isDefending;

	private Actor actor;
	private int itemsUsage = 0;
	[HideInInspector] public int defendCooldown;

	// METHODS

	public void Initialize() {
		actor = GetComponent<Actor>();
		if (actor.characterData.level == 1) itemsUsage = 10;
		ApplyHeldItemsEffects();
	}

	public void ExecuteAction() {
		
		string action = currentChoice.ToString();

		float chanceToFlinch;
		float oppIntimidation = actor.opponent.stats.intimidationPoints;

		if (oppIntimidation > actor.stats.intimidationPoints)
			chanceToFlinch = oppIntimidation * 3;
		else
			chanceToFlinch = 5;
		
		float hit = Random.Range(0, 100);

		if (hit < chanceToFlinch) {
			GetComponentInChildren<Animator>().Play("Intimidation");
			return;
		}

		switch (currentChoice)
		{
			case Actions.Attack:
				if (actor.stats.currentStaminaPoints < currentAttack.staminaCost) {
					RestAction();
					action = "Rest";
					break;
				}

				if(gameObject.tag == "Player") currentAttack.currentCooldown = currentAttack.cooldown+1;
				action = currentAttack.name;
				break;
			
			case Actions.Items:
				itemAnimationRenderer.sprite = currentItem.icon;
				bool isPlayer = (gameObject.tag == "Player");
				Inventory.instance.UseItem(currentItem, isPlayer);
				itemsUsage++;
				break;

			case Actions.Defend:
				defendCooldown = 2;
				isDefending = true;
				break;
			
			case Actions.Rest:
				RestAction();
				break;

			default:
				Debug.LogError("Actor::ExecuteAction --- Invalid Action.");
				return;
		}

		GetComponentInChildren<Animator>().Play(action);
	}

	private void RestAction() {
		actor.stats.RestoreHealth(10f);
		actor.stats.RestoreStamina(30f);
	}

	private void ApplyHeldItemsEffects() {
		if (actor.tag != "Player") return;
	
		if (Inventory.instance.WallEquipedObject != null) Inventory.instance.WallEquipedObject.Use(actor);
		if (Inventory.instance.BedEquipedObject != null) Inventory.instance.BedEquipedObject.Use(actor);
		if (Inventory.instance.LitterboxEquipedObject != null) Inventory.instance.LitterboxEquipedObject.Use(actor);
		if (Inventory.instance.ToyEquipedObject != null) Inventory.instance.ToyEquipedObject.Use(actor);
	}

	private Actions GetActionChoice() {
		float hp = actor.stats.currentHealthPoints;
		float oHp = actor.opponent.stats.currentHealthPoints;

		float sp = actor.stats.currentStaminaPoints;
		float oSp = actor.opponent.stats.currentStaminaPoints;

		float ap = actor.stats.speedPoints;
		float oAp = actor.opponent.stats.speedPoints;

		if (hp < oHp) {
			if ((oHp - hp) > 60) {
				if (sp > oSp) {
					return Random.value < 0.5 ? Actions.Attack : Actions.Defend;
				}

				return (Random.value < 0.7 || itemsUsage > 1) ? Actions.Attack : Actions.Items;
			}
			else {
				return Random.value < 0.8 ? Actions.Attack : Actions.Rest;
			}
		}
		
		return (Random.value < 0.5 || itemsUsage > 1) ? Actions.Attack : Actions.Items;
	}
}
