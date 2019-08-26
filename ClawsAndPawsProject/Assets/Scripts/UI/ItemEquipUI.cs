using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipUI : MonoBehaviour
{
    [SerializeField] private string itemType;
    public Image currentItemImage;

    [SerializeField] private InventoryEquipDisplayUI inventoryDisplay;

    private void Awake() {
        switch (GetItemType())
        {
            case HeldItemSO.EquipTypes.Wall:
                if (Inventory.instance.WallEquipedObject == null) return;

                currentItemImage.sprite = Inventory.instance.WallEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.Bed:
                if (Inventory.instance.BedEquipedObject == null) return;

                currentItemImage.sprite = Inventory.instance.BedEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.LitterBox:
                if (Inventory.instance.LitterboxEquipedObject == null) return;

                currentItemImage.sprite = Inventory.instance.LitterboxEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.Food:
                if (Inventory.instance.FoodEquipedObject == null) return;

                currentItemImage.sprite = Inventory.instance.FoodEquipedObject.icon;
                break;
            
            default:
                Debug.LogError("ItemEquipUI::Awake() --- Invalid HeldItemSO.EquipTypes type! Check your spelling in the itemType var.");
                return;
        }
    }

    public HeldItemSO.EquipTypes GetItemType() {
        return (HeldItemSO.EquipTypes)System.Enum.Parse( typeof(HeldItemSO.EquipTypes), itemType );
    }

    public void EnableInventoryDisplay() {
        inventoryDisplay.Enable(GetItemType());
    }
}
