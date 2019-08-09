using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public Image itemIcon;
	public Text itemName;
	public Text itemQuantity;
	public ItemSO itemData;

	private void OnEnable() {
		itemIcon.sprite = itemData.icon;
		itemName.text = itemData.name;
		GetComponent<Button>().onClick.AddListener(() => { FindObjectOfType<UIManager>().UseItem(itemData); } );
	}

	public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		dui.UpdateDescriptionText(itemData.description);
		dui.descriptionObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data) {
		FindObjectOfType<DescriptionsUI>().descriptionObject.SetActive(false);
	}
}
