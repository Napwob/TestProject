using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private List<LevelData> levels;
    [SerializeField] private GridController gridController;
    [SerializeField] private AnswerChecker answerChecker;
    [SerializeField] private Text taskDisplay;

    private int currentLevelIndex;

    private void Start()
    {
        LoadLevel(0);
    }

    public void LoadLevel(int index)
    {
        if (index >= levels.Count)
        {
            Debug.Log("No more levels to load.");
            return;
        }

        currentLevelIndex = index;
        LevelData levelData = levels[index];
        gridController.GenerateGrid(levelData, OnCellClicked);

        Sprite correctAnswer = levelData.AllowedSpriteGroups[0].Sprites[0]; 
        answerChecker.SetCorrectAnswer(correctAnswer);
        taskDisplay.text = $"Find {correctAnswer.name}";
    }

    private void OnCellClicked(CellController cell)
    {
        answerChecker.CheckAnswer(cell);
    }
}