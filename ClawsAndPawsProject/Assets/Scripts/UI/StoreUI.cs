using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private Text goldText;
    [SerializeField] private Text spentGoldText;

    private void Start() {
        Inventory.instance.Initialize();
    }

    private void Update() {
        goldText.text = Inventory.instance.gold.ToString();

    }

    public void Spend(int price) {
        spentGoldText.text = "-" + price;
        spentGoldText.gameObject.SetActive(true);
        spentGoldText.GetComponent<Animator>().Play("SpendText", -1, 0f);
        FindObjectOfType<AudioManager>().Play("Coins");
    }
}
