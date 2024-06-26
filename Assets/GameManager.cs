using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public string targetWord = "ICE"; // The word to be recognized
    public string currentInput = ""; // Current sequence input by player
    public float timeLimit = 2f;
    public int currentIndex = 0;

    public UnityEvent onWordCompleted; // UnityEvent to trigger when word is completed
    private float lastTimeInputChecked = 0f;

    // Function to reset the current input
    void ResetInput()
    {
        currentInput = "";
    }

    // Function to check if the current input matches the target word
    bool CheckInput(char letter)
    {   
        if (targetWord[currentIndex] == letter)
        {
            if(currentIndex == (targetWord.Length-1))
            {
                Debug.Log("Word completed: " + currentInput);
                onWordCompleted.Invoke(); // Trigger the event for word completion
                ResetInput(); // Reset current input for next word recognition
                currentIndex = 0;
            }
            return true;
        } else
        {
            Debug.Log("Wrong letter: " + letter);
            return false;
        }   
    }

    // Function to handle recognition of individual letters
    public void RecognizeLetter(string letter)
    {
        if (CheckInput(letter[0]))
        {
            currentInput += letter;
            currentIndex++;
            lastTimeInputChecked = Time.time;
        } else if (Time.time - lastTimeInputChecked > timeLimit)
        {
            ResetInput();
            currentIndex = 0;
        }
    }
}
