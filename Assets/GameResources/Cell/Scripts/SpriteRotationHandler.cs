namespace AmayaSoft.Cell
{
	using UnityEngine;
	using UnityEngine.UI;
	using System.Collections.Generic;

	public class SpriteRotationHandler : MonoBehaviour
	{
		[SerializeField] private bool needToRotate = false;

		[Header("Image Settings")]
		[SerializeField] private Image targetImage;
		[SerializeField, Range(-180, 180)] private float rotationAngle = 0f;
		[SerializeField] private List<string> spriteNamesToRotate = new List<string>();

		private void Start()
		{
			if (targetImage == null)
			{
				targetImage = GetComponent<Image>();
			}

			if (targetImage == null || targetImage.sprite == null)
			{
				Debug.LogError("Target Image or Sprite is not assigned.");
				return;
			}
		}

		private void OnEnable()
		{
			if (needToRotate)
			{
				rotateSpriteBasedOnName(targetImage.sprite.name);
			}
		}

		private void rotateSpriteBasedOnName(string spriteName)
		{
			Debug.Log(spriteName);
			if (spriteNamesToRotate.Contains(spriteName))
			{
				targetImage.rectTransform.rotation = Quaternion.Euler(0, 0, rotationAngle);
			}
			else
			{
				targetImage.rectTransform.rotation = Quaternion.identity;
			}
		}
	}
}