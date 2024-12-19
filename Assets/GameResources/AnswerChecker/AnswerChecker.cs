using UnityEngine;
using System;

public class AnswerChecker : MonoBehaviour
{
    public event Action OnCorrectAnswer;
    public event Action OnWrongAnswer;

    private Sprite correctAnswer;

    public void SetCorrectAnswer(Sprite answer)
    {
        correctAnswer = answer;
    }

    public void CheckAnswer(CellController cell)
    {
        if (cell != null && cell.GetSprite() == correctAnswer)
        {
            Debug.Log("Correct");
            OnCorrectAnswer?.Invoke();
        }
        else
        {
            Debug.Log("Incorrect");
            OnWrongAnswer?.Invoke();
        }
    }
}