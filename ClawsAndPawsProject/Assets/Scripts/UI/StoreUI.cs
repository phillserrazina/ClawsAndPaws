using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreUI : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Text goldText;

    private void Start() {
        Inventory.instance.Initialize();
    }

    private void Update() {
        goldText.text = Inventory.instance.gold.ToString();
    }
}
