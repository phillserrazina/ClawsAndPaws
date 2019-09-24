using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionsUI : MonoBehaviour
{
    public GameObject descriptionObject;
    [SerializeField] private Text nameText;
    [SerializeField] private Text descriptionText;
    [SerializeField] private Text goldText;

    public void UpdateDescriptionText(string n, string t) {
        nameText.text = n;
        descriptionText.text = t;
    }

    public void UpdateStoreDescriptionText(string n, string t, int price) {
        nameText.text = n;
        descriptionText.text = t;
        if (goldText != null) goldText.text = price.ToString();
    }
}
