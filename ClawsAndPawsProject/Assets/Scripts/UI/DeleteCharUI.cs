using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCharUI : MonoBehaviour
{
    public SaveSlotUI currentSlot { private get; set; }

    public void DeleteCharacter() {
        System.IO.File.Delete(currentSlot.assignedPath);
		currentSlot.TriggerNewCharacter();

        gameObject.SetActive(false);
    }
}
