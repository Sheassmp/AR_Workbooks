using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text nameText, dialogText;
    public GameObject StartBtn, ContinueBtn;
    private Queue<string> sentences;

    void Start(){
        sentences = new Queue<string>();
        ContinueBtn.SetActive(false);
    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("starting conversation with " + nameText);
        nameText.text = dialog.name;
        StartBtn.SetActive(false);
        ContinueBtn.SetActive(true);

        sentences.Clear();
        foreach (string sentence in dialog.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    

    IEnumerator TypeSentence (string sentence )
    {
        dialogText.text = "";
        foreach ( char letter in sentence.ToCharArray())
        {
            dialogText.text += letter;
            yield return null;
        }
    }

     void EndDialog()
    {
        Debug.Log("End Conversation");
        SceneManager.LoadScene(1);
    }
}
