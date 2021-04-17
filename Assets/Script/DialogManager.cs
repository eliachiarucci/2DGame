using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogText;
    public GameObject nameBox;
    public GameObject dialogBox;
    public static DialogManager instance;
    private InputMaster controls;
    public string[] dialogLines;
    public int currentLine = 0;

    private bool justStarted = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void DialogStep()
    {
        if (currentLine < dialogLines.Length && dialogLines.Length > 0)
        {
            if (dialogLines[currentLine].StartsWith("n-"))
            {
                nameText.text = dialogLines[currentLine].Replace("n-", "");
                currentLine++;
                DialogStep();
                return;
            } else
            {
                dialogText.text = dialogLines[currentLine];
            }
            currentLine++;
        } else
        {
            dialogBox.SetActive(false);
            currentLine = 0;
            PlayerController.instance.canMove = true;
        }
        
    }
   

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;
        currentLine = 0;
        justStarted = true;
        dialogBox.SetActive(true);
        PlayerController.instance.canMove = false;
    }
}
