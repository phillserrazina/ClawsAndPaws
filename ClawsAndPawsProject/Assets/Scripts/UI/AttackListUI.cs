using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListUI : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject itemList;

	private Actor player;
	public AttackSO[] allAttacks;

	private void Awake() {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Actor>();
	}

	private void OnEnable() {
		allAttacks = player.combat.attackList.attacks;

		for (int i = 0; i < allAttacks.Length; i++) {
			if (allAttacks[i].requiredLevel > player.characterData.level) continue;

			itemPrefab.GetComponent<AttackItem>().attackData = allAttacks[i];

			GameObject go = Instantiate(itemPrefab) as GameObject;
			go.transform.SetParent(itemList.transform);
			go.transform.localScale = Vector3.one;
		}
	}

	private void OnDisable() {
		for (int i = 0; i < itemList.transform.childCount; i++) {
			Destroy(itemList.transform.GetChild(i).gameObject);
		}
	}
}
