using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string sfxName;
    public bool changeSize = false;
    public float newSize;
    public bool changeSprite = false;
    public Sprite newSprite;

    private Sprite originalSprite;

    private void Awake() {
        originalSprite = GetComponent<Image>().sprite;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (sfxName.Length > 0)
            FindObjectOfType<AudioManager>().Play(sfxName);

        if (changeSize) transform.localScale *= newSize;
        if (changeSprite) GetComponent<Image>().sprite = newSprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (changeSize) transform.localScale /= newSize;
        if (changeSprite) GetComponent<Image>().sprite = originalSprite;
    }
}
