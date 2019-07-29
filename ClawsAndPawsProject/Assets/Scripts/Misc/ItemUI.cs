using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour {

	public Image itemIcon;
	public Text itemName;
	public ItemSO itemData;

	private void OnEnable() {
		itemIcon.sprite = itemData.icon;
		itemName.text = itemData.name;
		GetComponent<Button>().onClick.AddListener(() => { FindObjectOfType<UIManager>().UseItem(itemData); } );
	}
}
