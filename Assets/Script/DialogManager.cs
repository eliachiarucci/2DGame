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
    public string[] dialogLines;
    public int currentLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
            GameManager.instance.dialogActive = false;
        }

    }
   

    public void ShowDialog(string[] newLines)
    {
        dialogLines = newLines;
        currentLine = 0;
        dialogBox.SetActive(true);
        GameManager.instance.dialogActive = true;
    }
}
