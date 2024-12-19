using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButtonHandler : MonoBehaviour
{
    [SerializeField] private LevelLoader levelLoader;
    public Button ButtonInstance => button;

    protected Button button = default;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnRestartButtonPressed);
    }
    public void OnRestartButtonPressed()
    {
        if (levelLoader != null)
        {
            levelLoader.RestartGame();
        }
        else
        {
            Debug.LogError("LevelLoader is not assigned in RestartButtonHandler.");
        }
    }
}