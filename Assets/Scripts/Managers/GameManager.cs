using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject completeLevelUI;

    private void Awake()
    {
        GameIsOver = false;
    }

    private void Update()
    {
        if (GameIsOver)
            return;

        if (Input.GetKeyDown(KeyCode.T))
            GameOver();

        if (PlayerStats.Lives <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        GameIsOver = true;

        completeLevelUI.SetActive(true);
    }
}