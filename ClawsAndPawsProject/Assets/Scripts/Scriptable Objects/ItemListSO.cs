using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Items/Item List", fileName="New Item List")]
public class ItemListSO : ScriptableObject {

	public ItemSO[] items;

	public ItemSO Search(ItemSO item) {
		foreach (ItemSO i in items) {
			if (i.name.Equals(item.name)) return i;
		}

		return null;
	}

	public ItemSO Search(string itemName) {
		foreach (ItemSO i in items) {
			if (i.name.Equals(itemName)) return i;
		}

		Debug.LogError("ItemListSO::Search() --- " + itemName + " does not exist! Did you forget to add it to the list?");

		return null;
	}
}
