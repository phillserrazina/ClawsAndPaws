﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	// VARIABLES

	public List<ItemSO> keyItems = new List<ItemSO>();
	public List<HeldItemSO> heldItems = new List<HeldItemSO>();
	public List<ConsumableSO> consumableItems = new List<ConsumableSO>();

	private Actor player;

	// METHODS

	public void Initialize() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
	}

	public void Add(ItemSO item) {
		if (item is HeldItemSO) {
			heldItems.Add((HeldItemSO)item);
			return;
		}
		
		if (item is ConsumableSO) {
			consumableItems.Add((ConsumableSO)item);
			return;
		}

		keyItems.Add(item);
	}

	public void Remove(ItemSO item) {
		if (item is HeldItemSO) {
			heldItems.Remove((HeldItemSO)item);
			return;
		}
		
		if (item is ConsumableSO) {
			consumableItems.Remove((ConsumableSO)item);
			return;
		}

		keyItems.Remove(item);
	}

	public void UseItem(ItemSO item) {
		Remove(item);

		if (player == null)
			player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();

		item.Use(player);
	}
}
