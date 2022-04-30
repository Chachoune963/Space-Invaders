using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    public GameObject winUI;
    public GameObject loseUI;

    bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        winUI.SetActive(false);
        loseUI.SetActive(false);
        Controller.OnLoseDied += Lose;
        Enemy.OnWin += Win;
    }

    private void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Lose()
    {
        gameOver = true;
        if (loseUI != null) loseUI.SetActive(true);
    }

    private void Win()
    {
        gameOver = true;
        if (winUI != null) winUI.SetActive(true);
    }
}
