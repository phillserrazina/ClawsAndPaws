using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [Space(10)]
    [SerializeField] private GameObject inventoryButton;

    private bool isPaused = false;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            GameObject go = GameObject.Find("Inventory Menu");
            if (go != null) return;
            Pause();
        }
    }

    public void Pause() {
        isPaused = !isPaused;

        pauseButton.SetActive(!isPaused);
        pauseMenu.SetActive(isPaused);

        if (inventoryButton != null) inventoryButton.SetActive(!isPaused);
    }
}
