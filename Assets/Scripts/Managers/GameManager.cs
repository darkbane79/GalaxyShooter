using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private bool _isGameOver = false;

    [SerializeField]
    private GameObject _pauseMenuPanel;
    private Animator _pauseAnimator;

    private void Start()
    {
        _pauseAnimator = GameObject.Find("PauseMenuPanel").GetComponent<Animator>();
        if (_pauseAnimator == null)
        {
            Debug.LogError("Unable to find Animator");
        }
        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        _pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPause", true);
            Time.timeScale = 0;
        }



    }

    public void resumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void backToMain()
    {
        SceneManager.LoadScene(0);
    }



    public void isGameOver()
    {
        _isGameOver = true;
    }
    
   
    



}
