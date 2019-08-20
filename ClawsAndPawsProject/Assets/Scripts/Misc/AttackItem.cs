using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private Image itemIcon;
	[SerializeField] private Text attackName;
	public AttackSO attackData;

	private void OnEnable() {

		Button b = GetComponent<Button>();
		Stats pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

		itemIcon.sprite = attackData.attackIcon;
		attackName.text = attackData.name;

		if (b != null) {
			b.onClick.AddListener(() => { TurnOffDescriptions(); } );
			b.onClick.AddListener(() => { FindObjectOfType<UIManager>().SetPlayerAttack(attackData); } );
		}
		
		if (attackData.staminaCost > pStats.currentStaminaPoints) {
			b.interactable = false;
		}
	}

	public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		dui.UpdateDescriptionText(attackData.name, attackData.description);
		dui.descriptionObject.SetActive(true);
	}

	public void OnPointerExit(PointerEventData data) {
		TurnOffDescriptions();
	}

	private void TurnOffDescriptions() {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		if (dui != null) dui.descriptionObject.SetActive(false);
	}
}
