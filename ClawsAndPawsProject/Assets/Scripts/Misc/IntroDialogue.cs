using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
    [SerializeField] private string[] dialogueLines;
    private int currentLine = 0;

    private Text dialogueText;

    private void Start() {
        dialogueText = GetComponentInChildren<Text>();
    }

    private void Update() {
        dialogueText.text = dialogueLines[currentLine];
    }

    public void NextDialogue() {
        FindObjectOfType<AudioManager>().Play("Cat Meow");
        currentLine++;

        if (currentLine >= dialogueLines.Length) {
            SceneManager.LoadScene("HubScene");
        }

        currentLine = Mathf.Clamp(currentLine, 0, dialogueLines.Length-1);
    }

    public void PreviousDialogue() {
        FindObjectOfType<AudioManager>().Play("Cat Meow");
        currentLine--;
        currentLine = Mathf.Clamp(currentLine, 0, dialogueLines.Length-1);
    }
}
