using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackItem : MonoBehaviour {

	public Image itemIcon;
	public Text itemName;
	public AttackSO attackData;

	private void OnEnable() {

		Button b = GetComponent<Button>();
		Stats pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

		itemIcon.sprite = attackData.attackIcon;
		itemName.text = attackData.name;

		b.onClick.AddListener(() => { FindObjectOfType<UIManager>().SetPlayerAttack(attackData); } );

		if (attackData.staminaCost > pStats.currentStaminaPoints) {
			b.interactable = false;
		}
	}
}
