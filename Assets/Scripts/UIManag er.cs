using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject IntroUI;
    public GameObject ItemSpawner;

    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        IntroUI.SetActive(true);
        ItemSpawner.SetActive(false);
    }


    void Update()
    {
        if (GameManager.Instance.state == GameState.Playing)
        {
            ScoreText.text = "score: " + GameManager.Instance.CalculateScore();
            HighScoreText.text = "High Score: " + GameManager.Instance.HighScore;
        }
        else
        {
            ScoreText.text = "";
            HighScoreText.text = "";
        }
    }
}
