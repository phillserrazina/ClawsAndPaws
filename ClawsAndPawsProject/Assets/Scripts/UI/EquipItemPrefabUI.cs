﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EquipItemPrefabUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image itemIcon;
	public HeldItemSO itemData;

	private void OnEnable() {
		itemIcon.sprite = itemData.icon;

		Button b = GetComponent<Button>();
		if (b != null) {
			b.onClick.AddListener(() => { TurnOffDescriptions(); } );
			b.onClick.AddListener(() => { EquipItem(); } );
		}
	}

	public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		if (dui == null) return;
		dui.UpdateDescriptionText(itemData.name, itemData.description);
		dui.descriptionObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data) {
		TurnOffDescriptions();
	}

	private void TurnOffDescriptions() {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		if (dui != null) dui.descriptionObject.SetActive(false);
	}

    private void EquipItem() {
        Inventory.instance.EquipItem(itemData);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
