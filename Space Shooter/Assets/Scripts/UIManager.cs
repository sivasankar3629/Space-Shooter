using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _scoreText;
    [SerializeField]
    Sprite[] _livesSprites;
    [SerializeField]
    Image _actualLife;
    [SerializeField]
    TextMeshProUGUI _gameOverText;
    [SerializeField]
    TextMeshProUGUI _restartText;
    [SerializeField]
    GameManager _gameManager;
    
    void Start()
    {
        _scoreText.text = "Score : 0";
        _actualLife.sprite = _livesSprites[3];
    }
    
    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score : " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _actualLife.sprite = _livesSprites[currentLives];
        if (currentLives == 0)
        {
            GameOverSequence();
        }
    }
    
    void GameOverSequence()
    {
        _gameManager.GameOver();
         _gameOverText.gameObject.SetActive(true);
         _restartText.gameObject.SetActive(true);
         StartCoroutine(GameOverTextFlickerRoutine());
    }

    IEnumerator GameOverTextFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}