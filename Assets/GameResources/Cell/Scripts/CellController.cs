using UnityEngine;
using UnityEngine.UI;

public class CellController : MonoBehaviour
{
    [SerializeField] private Image symbolImage; 
    private Sprite cellData;

    public void Setup(Sprite sprite)
    {
        cellData = sprite;
        symbolImage.sprite = cellData;
    }

    public void OnCellClicked()
    {
        Destroy(gameObject);
    }
}