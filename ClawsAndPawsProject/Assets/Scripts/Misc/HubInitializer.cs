using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubInitializer : MonoBehaviour
{
    private void Start() {
        Inventory.instance.Initialize();
        SaveManager.Save(FindObjectOfType<CurrentCharacterManager>().currentCharacter);
    }
}
