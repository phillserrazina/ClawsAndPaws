using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string sfxName;
    public bool changeSize = false;
    public float newSize;
    public bool changeSprite = false;
    public Sprite newSprite;

    private Image image;
    private Vector3 originalSize;
    private Sprite originalSprite;

    private void Awake() {
        image = GetComponent<Image>();
        originalSize = transform.localScale;
        originalSprite = image.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (sfxName.Length > 0)
            FindObjectOfType<AudioManager>().Play(sfxName);

        if (changeSize) transform.localScale *= newSize;
        if (changeSprite) image.sprite = newSprite;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (changeSize && isAltered()) transform.localScale /= newSize;
        if (changeSprite && isAltered()) image.sprite = originalSprite;
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (changeSize && isAltered()) transform.localScale /= newSize;
        if (changeSprite && isAltered()) image.sprite = originalSprite;
    }

    private bool isAltered() {
        return (transform.localScale != originalSize || image.sprite != originalSprite);
    }
}
