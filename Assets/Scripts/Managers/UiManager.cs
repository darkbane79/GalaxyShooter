using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoSingleton<UiManager>
{
    [SerializeField]
    private Text _scoretext, _highScoreText, _ammoCountText;
    private int _score;
    [SerializeField]
    private int _highScore;
    [SerializeField]
    private GameObject _pauseMenuPanel;
    
    

    [SerializeField]
    private Sprite[] _p1LiveSprite;
    [SerializeField]
    private Image _p1CurrentLives;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    // Start is called before the first frame update
    void Start()
    {
        _score = 0;
        _scoretext.text = "Score : 0";
        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        _highScoreText.text = "High Score : " + _highScore;
        _ammoCountText.text = "Ammo : 15";
        //_p1LiveSprite[3];

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void checkHighScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
            PlayerPrefs.SetInt("HighScore", _highScore);
            _highScoreText.text = "High Score : " + _highScore;
            

        } 
    }

    public void UpdateScore(int score)
    {
        _score += score;
        _scoretext.text = "Score : " + _score;
        checkHighScore();

    }

    public void UpdateCurrentLives(int currentLives)
    {
        _p1CurrentLives.sprite = _p1LiveSprite[currentLives];

    }

    public void GameOver()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        GameManager.Instance.isGameOver();
        StartCoroutine(FlashGameover());
    }

    IEnumerator FlashGameover()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }

    }


    public void UpdateAmmo(int ammo)
    {
        _ammoCountText.text = "Ammo : " + ammo;
    }









}
