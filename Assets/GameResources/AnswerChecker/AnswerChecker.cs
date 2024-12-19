using UnityEngine;
using System;

public class AnswerChecker : MonoBehaviour
{
    [SerializeField] private ParticleSystem starParticles;
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
            starParticles.Play();
            return true;
        }
        else
        {
            animationHandler.Shake(cell.transform);
            return false;
        }
    }
}