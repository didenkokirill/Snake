using TMPro;
using UnityEngine;
using YG;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem Instance;

    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private int score;
    [SerializeField] private int bestScore;

    [SerializeField] private int foodCost;

    private void Awake() => Instance = this;

    public void SpawnPopUp(Vector2 position)
    {
        GameObject newPopup = Instantiate(popupPrefab, position, Quaternion.identity);
        newPopup.GetComponent<Popup>().Setup(foodCost);
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void AddScore()
    {
        score += foodCost;

        scoreText.text = score.ToString();

        if (score >= bestScore) 
        {
            SetBestScore(score);
        }
    }

    public void SetBestScore(int set)
    {
        bestScore = set;
        YandexGame.savesData.bestScore = bestScore;
    }
}
