using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationHandler : MonoBehaviour
{
    public void Bounce(Transform target)
    {
        target.DOPunchScale(Vector3.one * 0.2f, 0.3f, 1, 0.5f);
    }

    public void Shake(Transform target)
    {
        target.DOShakePosition(0.5f, 10, 10, 90, false, true);
    }

    public void FadeInImage(Image image, float alpha, TweenCallback onComplete = null)
    {
        image.DOFade(alpha, 0.5f).OnComplete(onComplete);
    }

    public void FadeOutImage(Image image)
    {
        image.DOFade(0f, 0.5f);
    }

    public void FadeInText(Text text)
    {
        text.canvasRenderer.SetAlpha(0);
        text.CrossFadeAlpha(1, 0.5f, false);
    }

    public void BounceIn(RectTransform target)
    {
        target.localScale = Vector3.zero;
        target.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBounce);
    }
}
