using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void LoadLevel1()
    {
        SceneManager.LoadSceneAsync(2); 
    }    public void LoadLevel2()
    {
        SceneManager.LoadSceneAsync(3); 
    }    public void LoadLevel3()
    {
        SceneManager.LoadSceneAsync(4); 
    }
}
