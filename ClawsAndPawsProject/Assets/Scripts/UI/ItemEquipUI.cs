using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipUI : MonoBehaviour
{
    [SerializeField] private string itemType;
    public Image currentItemImage;

    [SerializeField] private InventoryEquipDisplayUI inventoryDisplay;
    [SerializeField] private HeldItemSO firstItem;
    [SerializeField] private Animator animator;

    private void Awake() {
        switch (GetItemType())
        {
            case HeldItemSO.EquipTypes.Wall:
                if (Inventory.instance.WallEquipedObject == null) {
                    animator.enabled = true;
                    GetComponent<Animator>().Play("EquipItemFirstGlow");
                    return;
                } 

                currentItemImage.sprite = Inventory.instance.WallEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.Bed:
                if (Inventory.instance.BedEquipedObject == null) {
                    animator.enabled = true;
                    GetComponent<Animator>().Play("EquipItemFirstGlow");
                    return;
                } 

                currentItemImage.sprite = Inventory.instance.BedEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.LitterBox:
                if (Inventory.instance.LitterboxEquipedObject == null) {
                    animator.enabled = true;
                    GetComponent<Animator>().Play("EquipItemFirstGlow");
                    return;
                } 

                currentItemImage.sprite = Inventory.instance.LitterboxEquipedObject.icon;
                break;
            
            case HeldItemSO.EquipTypes.Toy:
                if (Inventory.instance.ToyEquipedObject == null) {
                    animator.enabled = true;
                    GetComponent<Animator>().Play("EquipItemFirstGlow");
                    return;
                } 

                currentItemImage.sprite = Inventory.instance.ToyEquipedObject.icon;
                break;
            
            default:
                Debug.LogError("ItemEquipUI::Awake() --- Invalid HeldItemSO.EquipTypes type! Check your spelling in the itemType var.");
                return;
        }
    }

    private void Update() {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
			GetComponent<Animator>().enabled = false;
		}
    }

    public HeldItemSO.EquipTypes GetItemType() {
        return (HeldItemSO.EquipTypes)System.Enum.Parse( typeof(HeldItemSO.EquipTypes), itemType );
    }

    public void EnableInventoryDisplay() {

        if (FirstItemHelper()) {
            SaveManager.Save(FindObjectOfType<CurrentCharacterManager>().currentCharacter);
            return;
        }

        inventoryDisplay.Enable(GetItemType());
    }

    private bool FirstItemHelper() {
        switch (GetItemType())
        {
            case HeldItemSO.EquipTypes.Wall:
                if (Inventory.instance.WallEquipedObject == null) {
                    Inventory.instance.Add(firstItem);
                    Inventory.instance.EquipItem(firstItem);
                    currentItemImage.sprite = Inventory.instance.WallEquipedObject.icon;
                    return true;
                } 

                return false;
            
            case HeldItemSO.EquipTypes.Bed:
                if (Inventory.instance.BedEquipedObject == null) {
                    Inventory.instance.Add(firstItem);
                    Inventory.instance.EquipItem(firstItem);
                    currentItemImage.sprite = Inventory.instance.BedEquipedObject.icon;
                    return true;
                } 

                return false;
            
            case HeldItemSO.EquipTypes.LitterBox:
                if (Inventory.instance.LitterboxEquipedObject == null) {
                    Inventory.instance.Add(firstItem);
                    Inventory.instance.EquipItem(firstItem);
                    currentItemImage.sprite = Inventory.instance.LitterboxEquipedObject.icon;

                    return true;
                } 

                return false;
            
            case HeldItemSO.EquipTypes.Toy:
                if (Inventory.instance.ToyEquipedObject == null) {
                    Inventory.instance.Add(firstItem);
                    Inventory.instance.EquipItem(firstItem);
                    currentItemImage.sprite = Inventory.instance.ToyEquipedObject.icon;
                    return true;
                } 

                return false;
            
            default:
                Debug.LogError("ItemEquipUI::Awake() --- Invalid HeldItemSO.EquipTypes type! Check your spelling in the itemType var.");
                return false;
        }
    }
}
