namespace AmayaSoft.Cell
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CellController : MonoBehaviour
    {
        [SerializeField] private Image symbolImage;
        private System.Action<CellController> onCellClicked;
        private Sprite cellData;

        public void Setup(Sprite sprite, System.Action<CellController> onClickAction)
        {
            cellData = sprite;
            symbolImage.sprite = sprite;
            onCellClicked = onClickAction;
        }

        public Sprite GetSprite()
        {
            return cellData;
        }

        public void OnClick()
        {
            onCellClicked?.Invoke(this);
        }
    }
}