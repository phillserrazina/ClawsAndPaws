using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionsUI : MonoBehaviour
{
    public GameObject descriptionObject;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text goldText;

    public void UpdateDescriptionText(string t) {
        descriptionText.text = t;
    }

    public void UpdateStoreDescriptionText(string t, int price) {
        descriptionText.text = t;
        goldText.text = price.ToString();
    }
}
