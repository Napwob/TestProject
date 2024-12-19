using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    [SerializeField] private Transform gridParent;
    [SerializeField] private ObjectPool cellPool;
    [SerializeField] private float cellSpacing = 10f;
    private AnimationHandler animationHandler;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
    }

    public List<Sprite> GenerateGrid(LevelData levelData, System.Action<CellController> onCellClicked)
    {
        ClearGrid();

        List<Sprite> gridSprites = new List<Sprite>();
        List<Sprite> availableSprites = GetAllAvailableSprites(levelData);
        float cellWidth = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.x;
        float cellHeight = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.y;

        float indentRow = (levelData.Rows - 1) * (cellWidth + cellSpacing);
        float indentCol = (levelData.Columns - 1) * (cellHeight + cellSpacing);

        for (int row = 0; row < levelData.Rows; row++)
        {
            for (int column = 0; column < levelData.Columns; column++)
            {
                if (availableSprites.Count == 0)
                {
                    Debug.LogError("Not enough unique sprites to fill the grid!");
                    break;
                }

                GameObject cell = cellPool.GetPooledObject();
                cell.transform.SetParent(gridParent, false);
                cell.SetActive(true);

                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(
                    (-indentCol)/2 + column * (cellWidth + cellSpacing),
                    indentRow/2 + (- row) * (cellHeight + cellSpacing)
                );

                animationHandler.BounceIn(rectTransform);

                Sprite sprite = availableSprites[Random.Range(0, availableSprites.Count)];
                availableSprites.Remove(sprite);
                gridSprites.Add(sprite);

                CellController cellController = cell.GetComponent<CellController>();
                cellController.Setup(sprite, onCellClicked);
            }
        }

        return gridSprites;
    }

    private List<Sprite> GetAllAvailableSprites(LevelData levelData)
    {
        List<Sprite> allSprites = new List<Sprite>();
        foreach (var group in levelData.AllowedSpriteGroups)
        {
            allSprites.AddRange(group.Sprites);
        }
        return allSprites;
    }

    private void ClearGrid()
    {
        foreach (Transform child in gridParent)
        {
            child.gameObject.SetActive(false);
        }
    }
}
