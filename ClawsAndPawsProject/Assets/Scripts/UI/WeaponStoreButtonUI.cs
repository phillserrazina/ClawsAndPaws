using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreButtonUI : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private int requiredLevel;
    [SerializeField] private HeldItemSO item;

    private void Update() {
        if (GetComponent<Button>().interactable == false) return;

        if (Inventory.instance.Contains(item.name) || 
            Inventory.instance.gold < price ||
            FindObjectOfType<CurrentCharacterManager>().currentCharacter.level < requiredLevel) {
                GetComponent<Button>().interactable = false;
        }
    }

    public void SellItem() {
        Inventory.instance.gold -= price;
        Inventory.instance.Add(item);
        SaveManager.Save(FindObjectOfType<CurrentCharacterManager>().currentCharacter);
    }
}
