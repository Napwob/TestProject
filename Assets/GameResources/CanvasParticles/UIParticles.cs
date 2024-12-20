namespace AmayaSoft.Particles
{
	using UnityEngine;
	using UnityEngine.UI;
	using DG.Tweening;
	using AmayaSoft.ObjectPools;
	using AmayaSoft.DGAnimations;

	public class UIParticles : MonoBehaviour
	{
		[Header("Particle Settings")]
		[SerializeField, Range(0, 30)] private int particlesNumber = 20;
		[SerializeField] private ObjectPool particlePool;
		[SerializeField] private float lifetime = 2f;
		[SerializeField] private Vector2 velocityRange = new Vector2(-50, 50);

		[Header("Animation Handler")]
		[SerializeField] private AnimationHandler animationHandler;

		public void SpawnParticles(Vector2 position)
		{
			for (int i = 0; i < particlesNumber; i++)
			{
				GameObject particle = particlePool.GetPooledObject();
				if (particle != null)
				{
					particle.transform.SetParent(gameObject.transform, false);
					particle.SetActive(true);

					RectTransform rectTransform = particle.GetComponent<RectTransform>();
					rectTransform.anchoredPosition = position;

					Vector2 randomVelocity = new Vector2(
						Random.Range(velocityRange.x, velocityRange.y),
						Random.Range(velocityRange.x, velocityRange.y)
					);

					animateParticle(particle, rectTransform, randomVelocity);
				}
			}
		}

		private void animateParticle(GameObject particle, RectTransform rectTransform, Vector2 velocity)
		{
			Image image = particle.GetComponent<Image>();
			Color initialColor = image.color;

			animationHandler.Bounce(rectTransform);

			rectTransform.DOAnchorPos(rectTransform.anchoredPosition + velocity * lifetime, lifetime)
				.SetEase(Ease.OutQuad)
				.OnComplete(() =>
				{
					particle.SetActive(false);
					image.color = initialColor;
				});

			animationHandler.FadeOutImage(image);
		}
	}
}
