using UnityEngine;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [SerializeField] private GridController gridController;
    [SerializeField] private AnswerChecker answerChecker;

    public List<Sprite> InitializeLevel(LevelData levelData, System.Action<CellController> onCellClicked)
    {
        return gridController.GenerateGrid(levelData, onCellClicked);
    }

    public void SetCorrectAnswer(Sprite correctAnswer)
    {
        answerChecker.SetCorrectAnswer(correctAnswer);
    }

    public bool CheckAnswer(CellController cell)
    {
        return answerChecker.CheckAnswer(cell);
    }
}

