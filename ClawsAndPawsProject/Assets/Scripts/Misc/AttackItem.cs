using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour {

	public Image itemIcon;
	public Text itemName;
	public AttackSO attackData;

	private void OnEnable() {
		itemIcon.sprite = attackData.attackIcon;
		itemName.text = attackData.name;
	}
}
