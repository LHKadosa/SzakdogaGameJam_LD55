using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
