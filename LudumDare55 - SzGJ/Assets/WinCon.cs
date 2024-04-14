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
    }

    public void CheckTowerNumber()
    {
        NumberOfTowers--;
        if (NumberOfTowers <= 0)
        {
            Debug.Log("There are no more towers left!");
            if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount - 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
