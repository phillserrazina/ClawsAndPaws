using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject itemList;

	public bool consumables = true;

	private void OnEnable() {

		if (consumables) {
			List<ConsumableSO> items = Inventory.instance.ConsumableItems;
			for (int i = 0; i < items.Count; i++) {
				InstantiateItem(items, i);
			}

			return;
		}
		
		List<HeldItemSO> heldItems = Inventory.instance.HeldItems;
		for (int i = 0; i < heldItems.Count; i++) {
			InstantiateItem(heldItems, i);
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
			if (i.itemName == item.itemName) answer++;
		}

		return answer;
	}

	private void InstantiateItem(List<ConsumableSO> items, int i) {
		itemPrefab.GetComponent<ItemUI>().itemData = items[i];

		int duplicateItems = FetchExistingQuantity(items, items[i]);

		if (duplicateItems > 0) {
			ItemUI[] allInventoryItems = FindObjectsOfType<ItemUI>();

			foreach (ItemUI dItem in allInventoryItems) {
				if (dItem.itemData.itemName == items[i].itemName) {
					dItem.itemQuantity.gameObject.SetActive(true);
					dItem.itemQuantity.text = "x" + duplicateItems.ToString();
					return;
				}
			}
		}

		GameObject go = Instantiate(itemPrefab) as GameObject;
		go.transform.SetParent(itemList.transform);
		go.transform.localScale = Vector3.one;
	}

	private void InstantiateItem(List<HeldItemSO> items, int i) {
		itemPrefab.GetComponent<ItemUI>().itemData = items[i];

		GameObject go = Instantiate(itemPrefab) as GameObject;
		go.transform.SetParent(itemList.transform);
		go.transform.localScale = Vector3.one;
	}
}
