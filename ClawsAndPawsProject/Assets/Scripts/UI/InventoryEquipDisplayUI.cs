using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquipDisplayUI : MonoBehaviour
{
    [SerializeField] private GameObject layoutGroup;
    [SerializeField] private GameObject itemPrefab;

    public void Enable(HeldItemSO.EquipTypes itemType) {
        var items = new List<HeldItemSO>();

        foreach (HeldItemSO item in Inventory.instance.HeldItems) {
            if (item.equipType == itemType) items.Add(item);
        }

        for (int i = 0; i < items.Count; i++) {
            InstantiateItem(items, i);
        }
    }

    private void OnDisable() {
		for (int i = 0; i < layoutGroup.transform.childCount; i++) {
			Destroy(layoutGroup.transform.GetChild(i).gameObject);
		}
	}

    private void InstantiateItem(List<HeldItemSO> items, int i) {
        itemPrefab.GetComponent<EquipItemPrefabUI>().itemData = items[i];

        GameObject go = Instantiate(itemPrefab) as GameObject;
        go.transform.SetParent(layoutGroup.transform);
        go.transform.localScale = Vector3.one;
    }
}
