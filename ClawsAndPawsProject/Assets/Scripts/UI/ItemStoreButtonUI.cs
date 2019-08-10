﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemStoreButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private int price;
    [SerializeField] private int requiredLevel;
    [SerializeField] private ConsumableSO item;

    private void Update() {
        if (GetComponent<Button>().interactable == false) return;

        if (Inventory.instance.gold < price ||
            FindObjectOfType<CurrentCharacterManager>().currentCharacter.level < requiredLevel) {
                GetComponent<Button>().interactable = false;
        }
    }

    public void SellItem() {
        Inventory.instance.gold -= price;
        Inventory.instance.Add(item);
        SaveManager.Save(FindObjectOfType<CurrentCharacterManager>().currentCharacter);
    }

    public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		dui.UpdateDescriptionText(item.description);
		dui.descriptionObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data) {
		FindObjectOfType<DescriptionsUI>().descriptionObject.SetActive(false);
	}
}
