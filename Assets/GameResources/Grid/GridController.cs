using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    [SerializeField] private GameObjectPool cellPool;
    [SerializeField, Range(1, 20)] private int cellSpacing = 10;

    public void GenerateGrid(LevelData levelData, System.Action<CellController> onCellClicked)
    {
        ClearGrid();

        float cellWidth = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.x;
        float cellHeight = cellPool.GetPooledObject().GetComponent<RectTransform>().sizeDelta.y;

        for (int row = 0; row < levelData.Rows; row++)
        {
            for (int column = 0; column < levelData.Columns; column++)
            {
                GameObject cell = cellPool.GetPooledObject();
                cell.transform.SetParent(gameObject.transform, false);
                cell.SetActive(true);

                RectTransform rectTransform = cell.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = new Vector2(
                    column * (cellWidth + cellSpacing),
                    -row * (cellHeight + cellSpacing)
                );

                Sprite sprite = GetSpriteByIndex(levelData, row * levelData.Columns + column);
                CellController cellController = cell.GetComponent<CellController>();
                cellController.Setup(sprite, onCellClicked);
            }
        }
    }

    private Sprite GetSpriteByIndex(LevelData levelData, int index)
    {
        int groupIndex = index % levelData.AllowedSpriteGroups.Count;
        SpriteGroup selectedGroup = levelData.AllowedSpriteGroups[groupIndex];
        int spriteIndex = index % selectedGroup.Sprites.Count;
        return selectedGroup.Sprites[spriteIndex];
    }

    private void ClearGrid()
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}