using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    public Text wordOutput = null;

    private string remainingWord = string.Empty;
    private string currentWord = "Apple";
    void Start()
    {
        SetCurrentWord();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }
    private void SetCurrentWord()
    {
        SetRemainingWord(currentWord);
    }
    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }
    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;
            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
        }
    }
    private void EnterLetter(string typeLetter)
    {
        if (IsCorrectLetter(typeLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
            {
                SetCurrentWord();
            }
        }
    }
    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }
    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }
    private bool IsWordComplete()
    {
        return remainingWord.Length==0;
    }
}
