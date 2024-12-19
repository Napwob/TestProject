namespace AmayaSoft.Grid
{
    using UnityEngine;
    using System.Collections.Generic;
    using AmayaSoft.Level;
    using AmayaSoft.DGAnimations;
    using AmayaSoft.ObjectPools;
    using AmayaSoft.Cell;

    public class GridController : MonoBehaviour
    {
        [Header("Grid Settings")]
        [SerializeField] private Transform gridParent;
        [SerializeField] private float cellSpacing = 10f;

        [Header("Object Pool")]
        [SerializeField] private ObjectPool cellPool;

        [Header("Animation Handler")]
        [SerializeField] private AnimationHandler animationHandler;

        public List<Sprite> GenerateGrid(LevelData levelData, System.Action<CellController> onCellClicked, bool isInitialLoad = false)
        {
            ClearGrid();

            SpriteGroup selectedGroup = GetRandomSpriteGroup(levelData);
            if (selectedGroup == null || selectedGroup.Sprites.Count == 0)
            {
                Debug.LogError("Invalid SpriteGroup provided. Cannot generate grid.");
                return new List<Sprite>();
            }

            return CreateGrid(selectedGroup.Sprites, levelData.Rows, levelData.Columns, onCellClicked, isInitialLoad);
        }

        private SpriteGroup GetRandomSpriteGroup(LevelData levelData)
        {
            if (levelData.AllowedSpriteGroups == null || levelData.AllowedSpriteGroups.Count == 0)
            {
                return null;
            }

            return levelData.AllowedSpriteGroups[Random.Range(0, levelData.AllowedSpriteGroups.Count)];
        }

        private List<Sprite> CreateGrid(List<Sprite> sprites, int rows, int columns, System.Action<CellController> onCellClicked, bool isInitialLoad)
        {
            List<Sprite> gridSprites = new List<Sprite>(sprites);
            List<Sprite> usedSprites = new List<Sprite>();
            float cellWidth = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.x;
            float cellHeight = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.y;

            float indentRow = (rows - 1) * (cellWidth + cellSpacing);
            float indentCol = (columns - 1) * (cellHeight + cellSpacing);

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (gridSprites.Count == 0)
                    {
                        Debug.LogError("Not enough unique sprites to fill the grid!");
                        break;
                    }

                    Sprite sprite = CreateCell(gridSprites, usedSprites, row, column, cellWidth, cellHeight, indentRow, indentCol, onCellClicked, isInitialLoad);
                    usedSprites.Add(sprite);
                }
            }

            SetCorrectAnswer(usedSprites);
            return usedSprites;
        }

        private Sprite CreateCell(List<Sprite> gridSprites, List<Sprite> usedSprites, int row, int column, float cellWidth, float cellHeight, float indentRow, float indentCol, System.Action<CellController> onCellClicked, bool isInitialLoad)
        {
            GameObject cell = cellPool.GetPooledObject();
            cell.transform.SetParent(gridParent, false);

            RectTransform rectTransform = cell.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(
                (-indentCol) / 2 + column * (cellWidth + cellSpacing),
                indentRow / 2 + (-row) * (cellHeight + cellSpacing)
            );

            Sprite sprite = gridSprites[Random.Range(0, gridSprites.Count)];
            gridSprites.Remove(sprite);

            CellController cellController = cell.GetComponent<CellController>();
            cellController.Setup(sprite, onCellClicked);
            cell.SetActive(true);

            if (isInitialLoad)
            {
                animationHandler.BounceIn(rectTransform);
            }

            return sprite;
        }

        private void SetCorrectAnswer(List<Sprite> usedSprites)
        {
            Sprite correctAnswer = usedSprites[Random.Range(0, usedSprites.Count)];
            //Debug.Log($"Correct Answer Selected: {correctAnswer.name}");
        }

        private void ClearGrid()
        {
            foreach (Transform child in gridParent)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
