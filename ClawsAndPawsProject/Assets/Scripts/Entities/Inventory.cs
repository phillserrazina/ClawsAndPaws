using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	// VARIABLES

	public List<ItemSO> keyItems = new List<ItemSO>();
	public List<HeldItemSO> heldItems = new List<HeldItemSO>();
	public List<ConsumableSO> consumableItems = new List<ConsumableSO>();

	// EXECUTION METHODS

	// METHODS

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
}
