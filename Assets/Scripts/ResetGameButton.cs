using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGameButton : MonoBehaviour
{
    public void ResetGameButtonClicked()
    {
        GameManager.ResetGame();
        SceneManager.LoadScene(0);
    }
}
