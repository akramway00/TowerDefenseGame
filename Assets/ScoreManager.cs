using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointdevie;
    [SerializeField] private Button restartButton;
    [SerializeField] private CanvasGroup restartButtonCanvasGroup;
    [SerializeField] private TMP_Text defaite;
    public float nombre;
    public bool cheatMode = false;
    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        HideRestartButton();
    }

    void Update()
    {
        pointdevie.text = " " + nombre;

        if (Input.GetKeyDown(KeyCode.P))
        {
            cheatMode = !cheatMode;  // active/désactive le mode triche
        }

        if (nombre <= 0f)
        {
            nombre = 0f;
            defaite.text = "VOUS AVEZ PERDU";
            ShowRestartButton();
        }
    }

    private void ShowRestartButton()
    {
        restartButtonCanvasGroup.alpha = 1;
        restartButtonCanvasGroup.interactable = true;
        restartButtonCanvasGroup.blocksRaycasts = true;
    }

    private void HideRestartButton()
    {
        restartButtonCanvasGroup.alpha = 0;
        restartButtonCanvasGroup.interactable = false;
        restartButtonCanvasGroup.blocksRaycasts = false;
    }

    public void DecreaseNombre(float amount)
    {
        if (!cheatMode)
        {
            nombre -= amount;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}