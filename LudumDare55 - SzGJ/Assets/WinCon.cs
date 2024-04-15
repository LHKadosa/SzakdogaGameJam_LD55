using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCon : MonoBehaviour
{
    [SerializeField] private int NumberOfTowers;
    void Start()
    {
        NumberOfTowers = GameObject.FindGameObjectsWithTag("Tower").Length;
        Debug.Log(SceneManager.sceneCountInBuildSettings);
    }

    public void CheckTowerNumber()
    {
        NumberOfTowers--;
        if (NumberOfTowers <= 0)
        {
            Debug.Log("There are no more towers left!");
            if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCountInBuildSettings - 1)
            {
                Debug.Log("Loading Next Level!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
