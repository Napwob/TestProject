namespace AmayaSoft.Level
{
	using UnityEngine;
	using System.Collections.Generic;
	using AmayaSoft.Cell;
	using AmayaSoft.Grid;

	public class LevelController : MonoBehaviour
	{
		[SerializeField] private GridController gridController;
		[SerializeField] private AnswerChecker answerChecker;

		public List<Sprite> InitializeLevel(LevelData levelData, System.Action<CellController> onCellClicked, bool isInitialLoad = false)
		{
			return gridController.GenerateGrid(levelData, onCellClicked, isInitialLoad);
		}

		public void SetCorrectAnswer(Sprite correctAnswer)
		{
			answerChecker.SetCorrectAnswer(correctAnswer);
		}

		public bool isClickedCellCorrect(CellController cell)
		{
			return answerChecker.CheckAnswer(cell);
		}
	}
}
