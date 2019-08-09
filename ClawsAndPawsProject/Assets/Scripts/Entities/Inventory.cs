using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory {

	public static Inventory instance = new Inventory();

	// VARIABLES

	public int gold;

	[SerializeField] private List<ItemSO> keyItems = new List<ItemSO>();
	public List<ItemSO> KeyItems { get { return keyItems; } }

	[SerializeField] private List<HeldItemSO> heldItems = new List<HeldItemSO>();
	public List<HeldItemSO> HeldItems { get { return heldItems; } }

	[SerializeField] private List<ConsumableSO> consumableItems = new List<ConsumableSO>();
	public List<ConsumableSO> ConsumableItems { get { return consumableItems; } }

	private Actor player;

	// METHODS	

	private void Clear() {
		gold = 0;
		keyItems.Clear();
		heldItems.Clear();
		consumableItems.Clear();
	}

	public void Initialize() {
		Clear();
		instance = this;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
		
		List<string> loadedInventory = SaveManager.LoadCurrentSaveData().inventoryData;

		if (loadedInventory.Count <= 0) return;

		gold = int.Parse(loadedInventory[0]);
		loadedInventory.RemoveAt(0);

		ItemListSO allItemList = Resources.Load("All Items") as ItemListSO;

		foreach (string n in loadedInventory) {
			Add(allItemList.Search(n));
		}
	}

	public List<string> GetInventoryData() {
		List<string> data = new List<string>();

		data.Add(gold.ToString());

		foreach (ItemSO i in keyItems) data.Add(i.name);
		foreach (ItemSO i in heldItems) data.Add(i.name);
		foreach (ItemSO i in consumableItems) data.Add(i.name);

		return data;
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

	public void Add(ItemSO[] items) {
		foreach (ItemSO item in items) {
			Add(item);
		}
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
