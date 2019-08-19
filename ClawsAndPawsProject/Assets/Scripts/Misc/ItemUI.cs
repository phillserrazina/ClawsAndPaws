using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {

	public Image itemIcon;
	public Text itemQuantity;
	public ItemSO itemData;

	private void OnEnable() {
		itemIcon.sprite = itemData.icon;

		Button b = GetComponent<Button>();
		if (b != null)
			b.onClick.AddListener(() => { FindObjectOfType<UIManager>().UseItem(itemData); } );
	}

	public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		dui.UpdateDescriptionText(itemData.name, itemData.description);
		dui.descriptionObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data) {
		FindObjectOfType<DescriptionsUI>().descriptionObject.SetActive(false);
	}

	public void OnPointerClick(PointerEventData data) {
		FindObjectOfType<DescriptionsUI>().descriptionObject.SetActive(false);
	}
}
