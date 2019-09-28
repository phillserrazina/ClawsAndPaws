using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRewardUI : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private GameObject itemList;

    private Stack<ItemSO> rewardsList = new Stack<ItemSO>();

    private void OnEnable() {
        if (rewardsList.Count <= 0) {
            gameObject.SetActive(false);
            return;
        }

        StartCoroutine(InstantiateItemCoroutine());
    }

    public void PushToRewardStack(ItemSO[] item) {
        foreach (ItemSO i in item) {
            rewardsList.Push(i);
        }
    }

    private IEnumerator InstantiateItemCoroutine() {
        if (rewardsList.Count > 0) {
            ItemSO currentItem = rewardsList.Pop();
            InstantiateItem(currentItem);

            yield return new WaitForSeconds(0.5f);

            StartCoroutine(InstantiateItemCoroutine());
        }
    }

    private void InstantiateItem(ItemSO item) {
		itemPrefab.GetComponent<ItemUI>().itemData = item;

		GameObject go = Instantiate(itemPrefab) as GameObject;
		go.transform.SetParent(itemList.transform);
		go.transform.localScale = Vector3.one;
	}
}
