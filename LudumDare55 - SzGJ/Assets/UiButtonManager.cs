using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Image Image_0;
    [SerializeField] Image Image_1;
    [SerializeField] Image Image_2;
    [SerializeField] Image Image_3;
    [SerializeField] Image Image_4;

    public void showSelection(int index)
    {
        //Debug.Log(index);

        Image_0.color = new Color(255, 255, 255);
        Image_1.color = new Color(255, 255, 255);
        Image_2.color = new Color(255, 255, 255);
        Image_3.color = new Color(255, 255, 255);
        Image_4.color = new Color(255, 255, 255);

        switch (index)
        {
            case 0: Image_0.color = new Color(0, 100, 150); break;
            case 1: Image_1.color = new Color(0, 100, 150); break;
            case 2: Image_2.color = new Color(0, 100, 150); break;
            case 3: Image_3.color = new Color(0, 100, 150); break;
            case 4: Image_4.color = new Color(0, 100, 150); break;
        }
    }
}
