using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject itemList;

	private void OnEnable() {
		List<ConsumableSO> items = Inventory.instance.ConsumableItems;

		for (int i = 0; i < items.Count; i++) {
			itemPrefab.GetComponent<ItemUI>().itemData = items[i];

			int duplicateItems = FetchExistingQuantity(items, items[i]);

			if (duplicateItems > 0) {
				ItemUI[] allInventoryItems = FindObjectsOfType<ItemUI>();

				foreach (ItemUI dItem in allInventoryItems) {
					if (dItem.itemData.name == items[i].name) {
						dItem.itemQuantity.text = "x" + duplicateItems.ToString();
						return;
					}
				}
			}

			GameObject go = Instantiate(itemPrefab) as GameObject;
			go.transform.SetParent(itemList.transform);
			go.transform.localScale = Vector3.one;
		}
	}

	private void OnDisable() {
		for (int i = 0; i < itemList.transform.childCount; i++) {
			Destroy(itemList.transform.GetChild(i).gameObject);
		}
	}

	private int FetchExistingQuantity(List<ConsumableSO> list, ConsumableSO item) {
		if (list.Count == 0) return 0;

		int answer = 0;

		foreach (ConsumableSO i in list) {
			if (i.name == item.name) answer++;
		}

		return answer;
	}
}
