using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonFX : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private string sfxName;

    public void OnPointerEnter(PointerEventData eventData) {
        FindObjectOfType<AudioManager>().Play(sfxName);
    }
}
