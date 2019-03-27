using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverText : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
        text.enabled = false;
    }
    private void OnGameEnded(bool isWin)
    {
        text.text = isWin ? "You Win!" : "Game Over";
        text.enabled = Settings.IsGameOverTextEnabled;
    }
    private void OnEnable()
    {
        GameManager.GameEnded += OnGameEnded;
    }
    private void OnDisable()
    {
        GameManager.GameEnded -= OnGameEnded;
    }
}
