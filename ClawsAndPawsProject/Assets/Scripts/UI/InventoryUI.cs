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
}
