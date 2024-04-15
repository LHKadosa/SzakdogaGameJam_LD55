using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    [Header("Lesson Canvas things")]
    public GameObject Lesson_1;
    public GameObject Lesson_2;
    public GameObject Lesson_3;
    public GameObject Lesson_4;
    public GameObject Lesson_5;
    public GameObject Lesson_6;
    public GameObject Lesson_7;
    public GameObject Lesson_8;
    public GameObject Lesson_9;
    public GameObject Lesson_10;
    public GameObject Lesson_11;

    [Header("Lesson Object things")]
    public GameObject Player;
    public GameObject Money;
    public GameObject UnitSelect;
    public GameObject PathTexture;
    public GameObject PathLogic;

    [Header("Lesson Package")]
    public GameObject Lesson_4_Things;
    public GameObject Lesson_7_Things;
    public GameObject Lesson_8_Things;
    public GameObject Lesson_9_Things;

    private int lessonCount = 1;
    private GameObject[] AllTargets;

    private void Start()
    {
        Lesson_1_Start();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            lessonCount++;

            switch (lessonCount)
            {
                case 2: Lesson_2_Start(); break;
                case 3: Lesson_3_Start(); break;
                case 4: Lesson_4_Start(); break;
                case 5: Lesson_5_Start(); break;
                case 6: Lesson_6_Start(); break;
                case 7: Lesson_7_Start(); break;
                case 8: Lesson_8_Start(); break;
                case 9: Lesson_9_Start(); break;
                case 10: Lesson_10_Start(); break;
                case 11: Lesson_11_Start(); break;
                case 12: Lesson_GoPlay(); break;
            }
        }
    }

    void Lesson_1_Start()
    {
        Lesson_1.SetActive(true);

        Player.SetActive(false);
        Money.SetActive(false);
        UnitSelect.SetActive(false);
        PathTexture.SetActive(false);
        PathLogic.SetActive(false);

        Lesson_4_Things.SetActive(false);
        Lesson_7_Things.SetActive(false);
        Lesson_8_Things.SetActive(false);
        Lesson_9_Things.SetActive(false);
    }

    void Lesson_2_Start()
    {
        Lesson_1.SetActive(false);
        Lesson_2.SetActive(true);

        Player.SetActive(true);
    }

    void Lesson_3_Start()
    {
        Lesson_2.SetActive(false);
        Lesson_3.SetActive(true);

        PathTexture.SetActive(true);
        PathLogic.SetActive(true);
    }

    void Lesson_4_Start()
    {
        Lesson_3.SetActive(false);
        Lesson_4.SetActive(true);

        DestroyAllUnit();

        Lesson_4_Things.SetActive(true);

        PathTexture.SetActive(false);
        PathLogic.SetActive(false);
    }

    void Lesson_5_Start()
    {
        Lesson_4.SetActive(false);
        Lesson_5.SetActive(true);

        Lesson_4_Things.SetActive(false);

        UnitSelect.SetActive(true);
        PathTexture.SetActive(true);
        PathLogic.SetActive(true);
    }

    void Lesson_6_Start()
    {
        Lesson_5.SetActive(false);
        Lesson_6.SetActive(true);

        DestroyAllUnit();
        Money.SetActive(true);
    }

    void Lesson_7_Start()
    {
        Lesson_6.SetActive(false);
        Lesson_7.SetActive(true);

        DestroyAllUnit();
        PathTexture.SetActive(false);
        PathLogic.SetActive(false);

        Lesson_7_Things.SetActive(true);
    }

    void Lesson_8_Start()
    {
        Lesson_7.SetActive(false);
        Lesson_8.SetActive(true);

        PathTexture.SetActive(true);
        PathLogic.SetActive(true);

        Lesson_7_Things.SetActive(false);
        Lesson_8_Things.SetActive(true);
    }

    void Lesson_9_Start()
    {
        Lesson_8.SetActive(false);
        Lesson_9.SetActive(true);

        DestroyAllUnit();

        Lesson_8_Things.SetActive(false);
        
    }

    void Lesson_10_Start()
    {
        Lesson_9.SetActive(false);
        Lesson_10.SetActive(true);

        Lesson_9_Things.SetActive(true); // 9 a neve de 10

        DestroyAllUnit();
    }

    void Lesson_11_Start()
    {
        Lesson_10.SetActive(false);
        Lesson_11.SetActive(true);

        DestroyAllUnit();
        PathTexture.SetActive(false);
        PathLogic.SetActive(false);

        Lesson_9_Things.SetActive(false);
    }

    void Lesson_GoPlay()
    {
        SceneManager.LoadScene(1);
    }

    void DestroyAllUnit()
    {
        AllTargets = GameObject.FindGameObjectsWithTag("Summon");

        foreach (GameObject currentTartget in AllTargets)
        {
            currentTartget.SetActive(false);
        }
    }
}
