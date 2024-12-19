using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<LevelData> levels;
    [SerializeField] private LevelController levelController;
    [SerializeField] private Text taskDisplay;
    [SerializeField] private Image fadeOverlay;
    [SerializeField] private GameObject restartButton;
    private AnimationHandler animationHandler;

    private int currentLevelIndex;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        restartButton.SetActive(false);
        fadeOverlay.enabled = false;
        fadeOverlay.color = new Color(0, 0, 0, 0);
        LoadLevel(0, true);
    }

    public void LoadLevel(int index, bool isInitialLoad = false)
    {
        if (index >= levels.Count)
        {
            EndGame();
            return;
        }

        currentLevelIndex = index;
        LevelData levelData = levels[index];

        List<Sprite> gridSprites = levelController.InitializeLevel(levelData, OnCellClicked, isInitialLoad);

        Sprite correctAnswer = gridSprites[Random.Range(0, gridSprites.Count)];
        levelController.SetCorrectAnswer(correctAnswer);

        taskDisplay.text = $"Find {correctAnswer.name}";
        if (isInitialLoad)
        {
            animationHandler.FadeInText(taskDisplay);
        }
    }

    private void OnCellClicked(CellController cell)
    {
        if (levelController.isClickedCellCorrect(cell))
        {
            LoadLevel(currentLevelIndex + 1);
        }
    }

    private void EndGame()
    {
        restartButton.SetActive(true);
        fadeOverlay.enabled = true;
        fadeOverlay.raycastTarget = true;
        animationHandler.FadeInImage(fadeOverlay, 0.9f, null);
    }

    public void RestartGame()
    {
        animationHandler.FadeInImage(fadeOverlay, 1f, () =>
        {
            restartButton.SetActive(false);
            LoadLevel(0, true);
            animationHandler.FadeOutImage(fadeOverlay);
            fadeOverlay.enabled = false;
        });
    }
}