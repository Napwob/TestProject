using UnityEngine;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] private UIParticles uiParticles;
    private AnimationHandler animationHandler;

    private Sprite correctAnswer;

    private void Awake()
    {
        animationHandler = GetComponent<AnimationHandler>();
    }

    public void SetCorrectAnswer(Sprite answer)
    {
        correctAnswer = answer;
    }

    public bool CheckAnswer(CellController cell)
    {
        if (cell != null && cell.GetSprite().name == correctAnswer.name)
        {
            animationHandler.Bounce(cell.transform);

            Vector2 cellPosition = cell.transform.localPosition;
            uiParticles.SpawnParticles(cellPosition);

            return true;
        }
        else
        {
            animationHandler.Shake(cell.transform);
            return false;
        }
    }
}
