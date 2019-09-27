using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AttackItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	[SerializeField] private Image itemIcon;
	[SerializeField] private Text attackName;
	[SerializeField] private GameObject cooldownMarker;
	public AttackSO attackData;

	private Stats pStats;	// Player stats
	private Image staminaImage;	// Player Stamina Image

	private void OnEnable() {

		Button b = GetComponent<Button>();
		pStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();

		staminaImage = GameObject.Find("Left Stamina Background").GetComponent<Image>();

		itemIcon.sprite = attackData.attackIcon;
		attackName.text = attackData.name;
		cooldownMarker.GetComponentInChildren<Text>().text = attackData.currentCooldown.ToString();

		if (attackData.currentCooldown > 0) {
			cooldownMarker.SetActive(true);
			b.interactable = false;
		}

		if (b != null) {
			b.onClick.AddListener(() => { TurnOffDescriptions(); } );
			b.onClick.AddListener(() => { FindObjectOfType<UIManager>().SetPlayerAttack(attackData); } );
		}

		attackName.color = Color.white;
	}

	public void OnPointerEnter(PointerEventData data) {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		dui.UpdateDescriptionText(attackData.name, attackData.description);
		dui.descriptionObject.SetActive(true);

		if (attackData.staminaCost > pStats.currentStaminaPoints) {
			Color color = new Color(0.8f, 0.1f, 0.1f, 1f);
			attackName.color = color;
			staminaImage.color = color;
		}
	}

	public void OnPointerExit(PointerEventData data) {
		TurnOffDescriptions();
		attackName.color = Color.white;
		staminaImage.color = Color.black;
	}

	private void TurnOffDescriptions() {
		DescriptionsUI dui = FindObjectOfType<DescriptionsUI>();
		if (dui != null) dui.descriptionObject.SetActive(false);

		staminaImage.color = Color.black;
	}
}
