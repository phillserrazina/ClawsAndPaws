using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionsUI : MonoBehaviour
{
    public GameObject descriptionObject;
    [SerializeField] private Text descriptionText;

    public void UpdateDescriptionText(string t) {
        descriptionText.text = t;
    }
}
