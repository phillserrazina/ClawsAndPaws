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

	[SerializeField] private HeldItemSO wallEquipedObject = null;
	public HeldItemSO WallEquipedObject { get { return wallEquipedObject; } }
	[SerializeField] private HeldItemSO bedEquipedObject = null;
	public HeldItemSO BedEquipedObject { get { return bedEquipedObject; } }
	[SerializeField] private HeldItemSO litterboxEquipedObject = null;
	public HeldItemSO LitterboxEquipedObject { get { return litterboxEquipedObject; } }
	[SerializeField] private HeldItemSO toyEquipedObject = null;
	public HeldItemSO ToyEquipedObject { get { return toyEquipedObject; } }

	private Actor player;

	// METHODS	

	public void Initialize() {
		Clear();
		instance = this;
		GameObject go = GameObject.FindGameObjectWithTag("Player");
		if (go != null)
			player = go.GetComponent<Actor>();
		
		List<string> loadedInventory = SaveManager.LoadCurrentSaveData().inventoryData;

		if (loadedInventory.Count <= 0) return;

		gold = int.Parse(loadedInventory[0]);
		loadedInventory.RemoveAt(0);

		ItemListSO allItemList = Resources.Load("All Items") as ItemListSO;

		if (loadedInventory[0] != "null")
			wallEquipedObject = allItemList.Search(loadedInventory[0]) as HeldItemSO;
		loadedInventory.RemoveAt(0);

		if (loadedInventory[0] != "null")
			bedEquipedObject = allItemList.Search(loadedInventory[0]) as HeldItemSO;
		loadedInventory.RemoveAt(0);

		if (loadedInventory[0] != "null")
			litterboxEquipedObject = allItemList.Search(loadedInventory[0]) as HeldItemSO;
		loadedInventory.RemoveAt(0);

		if (loadedInventory[0] != "null")
			toyEquipedObject = allItemList.Search(loadedInventory[0]) as HeldItemSO;
		loadedInventory.RemoveAt(0);

		foreach (string n in loadedInventory) {
			Add(allItemList.Search(n));
		}
	}

	public List<string> GetInventoryData() {
		List<string> data = new List<string>();

		data.Add(gold.ToString()); 
		data.Add((wallEquipedObject == null) ? "null" : wallEquipedObject.name);
		data.Add((bedEquipedObject == null) ? "null" : bedEquipedObject.name);
		data.Add((litterboxEquipedObject == null) ? "null" : litterboxEquipedObject.name);
		data.Add((toyEquipedObject == null) ? "null" : toyEquipedObject.name);

		foreach (ItemSO i in keyItems) {
			if (i == null) continue;
			data.Add(i.name);
		}
		foreach (ItemSO i in heldItems) {
			if (i == null) continue;
			data.Add(i.name);
		}
		foreach (ItemSO i in consumableItems) {
			if (i == null) continue;
			data.Add(i.name);
		}

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
		if (items.Length <= 0) return;

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

		player.stats.AddEffect(item as ConsumableSO);
	}

	private void Clear() {
		gold = 0;
		keyItems.Clear();
		heldItems.Clear();
		consumableItems.Clear();
	}

	public bool Contains(ItemSO item) {
		if (item is HeldItemSO) {
			if (heldItems.Contains((HeldItemSO)item)) return true;
		}
		
		if (item is ConsumableSO) {
			if (consumableItems.Contains((ConsumableSO)item)) return true;
		}

		if (keyItems.Contains(item)) return true;
		
		return false;
	}

	public bool Contains(string item) {
		foreach (HeldItemSO i in heldItems) {
			if (i == null) continue;
			if (i.name == item) return true;
		}
		
		foreach (ConsumableSO i in consumableItems) {
			if (i == null) continue;
			if (i.name == item) return true;
		}

		foreach (ItemSO i in keyItems) {
			if (i == null) continue;
			if (i.name == item) return true;
		}
		
		return false;
	}

	public void EquipItem(HeldItemSO itemData) {
		switch (itemData.equipType)
        {
            case HeldItemSO.EquipTypes.Wall:
                wallEquipedObject = itemData;
                break;
            
            case HeldItemSO.EquipTypes.Bed:
                bedEquipedObject = itemData;
                break;
            
            case HeldItemSO.EquipTypes.LitterBox:
                litterboxEquipedObject = itemData;
                break;
            
            case HeldItemSO.EquipTypes.Toy:
                toyEquipedObject = itemData;
                break;
            
            default:
                Debug.LogError("Inventory::EquipItem() --- Invalid HeldItemSO.EquipTypes type!");
                return;
        }
	}
}
