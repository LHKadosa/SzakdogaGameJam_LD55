using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    [Header("Image references")]
    [SerializeField] private Image Image_0;
    [SerializeField] private Image Image_1;
    [SerializeField] private Image Image_2;
    [SerializeField] private Image Image_3;
    [SerializeField] private Image Image_4;

    [Header("Face references")]
    [SerializeField] private Image Imafe_Face_Normal;
    [SerializeField] private Image Imafe_Face_Happy;
    [SerializeField] private Image Imafe_Face_Sad;

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

    public void beHappy()
    {
        if (Imafe_Face_Normal.enabled == true)
        {
            Imafe_Face_Normal.enabled = false;
            Imafe_Face_Sad.enabled = false;
            Imafe_Face_Happy.enabled = true;
            StartCoroutine(beNormal());
        }
    }

    public void beSad()
    {
        if (Imafe_Face_Normal.enabled == true)
        {
            Imafe_Face_Normal.enabled = false;
            Imafe_Face_Happy.enabled = false;
            Imafe_Face_Sad.enabled = true;
            StartCoroutine(beNormal());
        }
    }

    IEnumerator beNormal()
    {
        yield return new WaitForSeconds(1f);
        Imafe_Face_Happy.enabled = false;
        Imafe_Face_Sad.enabled = false;
        Imafe_Face_Normal.enabled = true;
    }
}
