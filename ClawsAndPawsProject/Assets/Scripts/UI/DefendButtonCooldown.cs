using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefendButtonCooldown : MonoBehaviour
{
    [SerializeField] private GameObject cooldownObject;
    private Combat player;

    private void OnEnable() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
        
        if (player.defendCooldown > 0) {
            GetComponent<Button>().interactable = false;
            cooldownObject.SetActive(true);
            cooldownObject.GetComponentInChildren<Text>().text = player.defendCooldown.ToString();
        }
        else {
            GetComponent<Button>().interactable = true;
            cooldownObject.GetComponentInChildren<Text>().text = player.defendCooldown.ToString();
            cooldownObject.SetActive(false);
        }
    }
}
